using BusinessLayer.Validation;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RealEstate.Extensions;
using RealEstate.Middlewares;
using Shared.Dtos;
using Shared.Services;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.DependenciesContainer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MatchEstateDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServerDevelopment"]));
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddAntiforgery();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "UserScheme";
})
.AddCookie("UserScheme", options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
})
.AddCookie("AdminScheme", options =>
{
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Admin/AccessDenied";
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddIdentity<User, Role>(x =>
{
    x.Password.RequireNonAlphanumeric = false;
})
.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
.AddEntityFrameworkStores<MatchEstateDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireAuthenticatedUser()
        .AddAuthenticationSchemes("AdminScheme")
        .RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddHostedService<InactiveUserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<MatchEstateDbContext>();
        db.Database.Migrate();
    }
}

app.UseStatusCodePagesWithReExecute("/Error/ErrorPage", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserActivityMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "Account/{controller=Login}/{action=Index}/{id?}"
);

app.MapGet("/getStatistics", (IStatisticsRepository statisticsDal, ClaimsPrincipal user) =>
{
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var propertyTypesOfListings = statisticsDal.GetPropertyTypesOfListings(userId);
    var propertyTypesOfRequests = statisticsDal.GetPropertyTypesOfRequests(userId);
    var ForSaleOrRentOfListings = statisticsDal.GetPropertyStatusesOfListings(userId);
    var ForSaleOrRentOfRequests = statisticsDal.GetPropertyStatusesOfRequests(userId);

    return new StatisticsModelDTO()
    {
        CountOfApartmentListings = propertyTypesOfListings.Where(o => o.PropertyTypeId == 5).FirstOrDefault()?.Count ?? 0,
        CountOfLandListings = propertyTypesOfListings.Where(o => o.PropertyTypeId == 2).FirstOrDefault()?.Count ?? 0,
        CountOfFarmlandListings = propertyTypesOfListings.Where(o => o.PropertyTypeId == 4).FirstOrDefault()?.Count ?? 0,
        CountOfCommercialUnitListings = propertyTypesOfListings.Where(o => o.PropertyTypeId == 3).FirstOrDefault()?.Count ?? 0,
        CountOfShopListings = propertyTypesOfListings.Where(o => o.PropertyTypeId == 1).FirstOrDefault()?.Count ?? 0,

        CountOfApartmentRequests = propertyTypesOfRequests.Where(o => o.PropertyTypeId == 5).FirstOrDefault()?.Count ?? 0,
        CountOfLandRequests = propertyTypesOfRequests.Where(o => o.PropertyTypeId == 2).FirstOrDefault()?.Count ?? 0,
        CountOfFarmlandRequests = propertyTypesOfRequests.Where(o => o.PropertyTypeId == 4).FirstOrDefault()?.Count ?? 0,
        CountOfCommercialUnitRequests = propertyTypesOfRequests.Where(o => o.PropertyTypeId == 3).FirstOrDefault()?.Count ?? 0,
        CountOfShopRequests = propertyTypesOfRequests.Where(o => o.PropertyTypeId == 1).FirstOrDefault()?.Count ?? 0,

        CountOfListingsPropertyStatuses = ForSaleOrRentOfListings,
        CountOfRequestsPropertyStatuses = ForSaleOrRentOfRequests,
    };
});

app.Run();