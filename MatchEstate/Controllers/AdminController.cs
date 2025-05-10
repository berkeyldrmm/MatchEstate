using Azure;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchEstate.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IValidator<LoginModelDTO> _validator;
        private readonly IValidator<AddUserDTO> _addUserModelValidator;
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, IValidator<LoginModelDTO> validator, IPropertyListingService listingService, IPropertyRequestService requestService, IValidator<AddUserDTO> addUserModelValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validator = validator;
            _listingService = listingService;
            _requestService = requestService;
            _addUserModelValidator = addUserModelValidator;
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

            });

            IEnumerable<AdminPageUserModel> admins = _userManager.GetUsersInRoleAsync("Admin").Result.Select(u => new AdminPageUserModel
            {
                Id = u.Id,
                Email = u.Email,
                NameSurname = u.NameSurname,
                PhoneNumber = u.PhoneNumber,
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
        public async Task<IActionResult> Login([FromForm] LoginModelDTO model)
        {
            var validateResult = await _validator.ValidateAsync(model);
            if (validateResult.IsValid)
            {
                User? user = await _userManager.FindByNameAsync(model.Mail);
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password) || !await _userManager.IsInRoleAsync(user, "Admin"))
                {

                    ViewBag.loginFail = "Email or password invalid.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _signInManager.Context.SignInAsync("AdminScheme", claimsPrincipal, new AuthenticationProperties() { IsPersistent = model.RememberMe});

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
                UserNameSurname = user,
                Listings = _listingService.GetAllWithClient(id).Result.Select(l => new AdminPageListingModel { Title = l.Title, PropertyType = l.PropertyType.PropertyName, Status = l.Status }),
                Requests = _requestService.GetAllWithClient(id).Result.Select(r => new AdminPageRequestModel { Title = r.Title, PropertyType = r.PropertyType.PropertyName })
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
            await _signInManager.Context.SignOutAsync("AdminScheme");
            return RedirectToAction("Index");
        }
    }
}
