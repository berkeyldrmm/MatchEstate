using BusinessLayer.Validation;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RealEstate.Extensions;
using RealEstate.Middlewares;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.DependenciesContainer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MatchEstateDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null
                       );
                }));
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidation>();
builder.Services.AddScoped<IValidator<AddListingDTO>, ListingModelValidator>();

builder.Services.AddAuthentication(
               CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(x =>
               {
                   x.Cookie.Name = "Auth";
                   x.LoginPath = "/login";
                   x.AccessDeniedPath = "/login";
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
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("admin");
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/ErrorPage", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "Account/{controller=Login}/{action=Index}/{id?}"
);

app.MapGet("/getStatistics", (IStatisticsDal statisticsDal, ClaimsPrincipal user) =>
{
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var propertyTypesOfListings = statisticsDal.GetPropertyTypesOfListings(userId);
    var propertyTypesOfRequests = statisticsDal.GetPropertyTypesOfRequests(userId);
    var ForSaleOrRentOfListings = statisticsDal.GetForSaleOrRentOfListings(userId);
    var ForSaleOrRentOfRequests = statisticsDal.GetForSaleOrRentOfRequests(userId);

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

        CountOfListingsForRent = ForSaleOrRentOfListings.Where(o => o.ForSaleOrRent == "For Rent").FirstOrDefault()?.Count ?? 0,
        CountOfListingsForSale = ForSaleOrRentOfListings.Where(o => o.ForSaleOrRent == "For Sale").FirstOrDefault()?.Count ?? 0,

        CountOfRequestsForRent = ForSaleOrRentOfRequests.Where(o => o.ForSaleOrRent == "For Rent").FirstOrDefault()?.Count ?? 0,
        CountOfRequestsForSale = ForSaleOrRentOfRequests.Where(o => o.ForSaleOrRent == "For Sale").FirstOrDefault()?.Count ?? 0
    };
});

app.Run();
