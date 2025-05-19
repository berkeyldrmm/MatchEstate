using Azure;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Abstract;
using Shared.Dtos;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BusinessLayer.Services;
using Shared.Dtos.Auth;

namespace MatchEstate.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly IValidator<AddUserDTO> _addUserModelValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordDtoValidator;
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        private readonly IStatisticsRepository _statisticsRepository;
        public AdminController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IValidator<LoginDto> loginDtoValidator,
            IPropertyListingService listingService,
            IPropertyRequestService requestService,
            IValidator<AddUserDTO> addUserModelValidator,
            IValidator<ChangePasswordDto> changePasswordDtoValidator,
            IStatisticsRepository statisticsRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loginDtoValidator = loginDtoValidator;
            _listingService = listingService;
            _requestService = requestService;
            _addUserModelValidator = addUserModelValidator;
            _changePasswordDtoValidator = changePasswordDtoValidator;
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Admin Page";

            IEnumerable<AdminPageUserModel> users = _userManager.Users.Select(u => new AdminPageUserModel
            {
                Id = u.Id,
                Email = u.Email,
                NameSurname = u.NameSurname,
                PhoneNumber = u.PhoneNumber,
                Activity = u.IsOnline,
                LastActiveDate = u.LastActiveDate
            });

            IEnumerable<AdminPageUserModel> admins = _userManager.GetUsersInRoleAsync("Admin").Result.Select(u => new AdminPageUserModel
            {
                Id = u.Id,
                Email = u.Email,
                NameSurname = u.NameSurname,
                PhoneNumber = u.PhoneNumber,
                Activity = u.IsOnline,
                LastActiveDate = u.LastActiveDate
            });

            return View(new AdminUserViewModel { Users = users, Admins = admins });
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.title = "Login Page";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            var validateResult = await _loginDtoValidator.ValidateAsync(model);
            if (validateResult.IsValid)
            {
                User? user = await _userManager.Users.Where(u => u.Email == model.UsernameOrMail || u.UserName == model.UsernameOrMail).FirstOrDefaultAsync();
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password) || !await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    ViewBag.error = "Email or password invalid.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _signInManager.Context.SignInAsync("AdminScheme", claimsPrincipal, new AuthenticationProperties() { IsPersistent = model.RememberMe});

                var localeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
                user.LastActiveDate = localeNow;
                user.IsOnline = true;
                await _userManager.UpdateAsync(user);
                
                TempData["adminNameSurname"] = user.NameSurname;
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
                ViewBag.error = ValidationMessageWriter.MessageWriter(allErrors);
            }

            return View();
        }

        public IActionResult UserDetails(string id)
        {
            ViewBag.title = "User Details";

            string user = _userManager.FindByIdAsync(id).Result.NameSurname;

            var listingRequestModel = new ListingRequestModel()
            {
                UserId = id,
                UserNameSurname = user,
                Listings = _listingService.GetAllWithClient(id).Result.Select(l => new AdminPageListingModel { Title = l.Title, PropertyType = l.PropertyType.PropertyName, Status = l.DealStatus, PropertyStatus = l.PropertyStatus.Name }),
                Requests = _requestService.GetAllWithClient(id).Result.Select(r => new AdminPageRequestModel { Title = r.Title, PropertyType = r.PropertyType.PropertyName, PropertyStatus = r.PropertyStatus.Name })
            };

            return View(listingRequestModel);
        }

        public IActionResult AddUser()
        {
            ViewBag.title = "Add User";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDTO model)
        {
            var validateResult = _addUserModelValidator.Validate(model);
            if (validateResult.IsValid) {
                User user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = model.NameSurname,
                    Email = model.Mail,
                    UserName = model.Mail,
                    PhoneNumber = model.PhoneNumber
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "New user has been added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = result.Errors.Select(e => e.Description);
                }
            }

            ViewBag.error = validateResult.Errors.Select(x => x.ErrorMessage);
            return View();
        }
        public IActionResult AddAdmin()
        {
            ViewBag.title = "Add admin";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddUserDTO model)
        {
            var validateResult = _addUserModelValidator.Validate(model);
            if (validateResult.IsValid)
            {
                User user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = model.NameSurname,
                    UserName = model.Mail,
                    Email = model.Mail,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var result2 = await _userManager.AddToRoleAsync(user, "Admin");
                    if(result2.Succeeded)
                    {
                        TempData["success"] = "New admin has been added successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error=result2.Errors.Select(e => e.Description);
                    }
                }
                else
                {
                    ViewBag.error = result.Errors.Select(e => e.Description);
                }
            }
            else
            {
                ViewBag.error = validateResult.Errors.Select(x => x.ErrorMessage);
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            User user = await _userManager.GetUserAsync(User);
            await _signInManager.Context.SignOutAsync("AdminScheme");
            
            var localeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
            user.LastActiveDate = localeNow;
            user.IsOnline = false;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public IActionResult ChangePassword()
        {
            ViewBag.title = "Change Password Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var validateResult = await _changePasswordDtoValidator.ValidateAsync(dto);
            if (validateResult.IsValid)
            {
                User? user = await _userManager.FindByIdAsync(UserId);
                var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                if (result.Succeeded)
                {
                    TempData["success"] = "Password has been changed successfully.";
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.error = ValidationMessageWriter.MessageWriter(result.Errors.Select(x => x.Description));
            }
            else
            {
                ViewBag.error = ValidationMessageWriter.MessageWriter(validateResult.Errors.Select(err => err.ErrorMessage));
            }

            return View();
        }

        public IActionResult GetUserStatistics(string userId)
        {
            var propertyTypesOfListings = _statisticsRepository.GetPropertyTypesOfListings(userId);
            var propertyTypesOfRequests = _statisticsRepository.GetPropertyTypesOfRequests(userId);
            var ForSaleOrRentOfListings = _statisticsRepository.GetPropertyStatusesOfListings(userId);
            var ForSaleOrRentOfRequests = _statisticsRepository.GetPropertyStatusesOfRequests(userId);

            return Ok(new StatisticsModelDTO()
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
            });
        }
    }
}
