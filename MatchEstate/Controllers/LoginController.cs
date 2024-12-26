
using DataAccessLayer.Context;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchEstate.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Login Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginModelDTO model)
        {
            if(ModelState.IsValid)
            {
                User? user = await _userManager.FindByNameAsync(model.Mail);
                if (user == null)
                {
                    ViewBag.error = "Email or password invalid.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await _signInManager.SignInWithClaimsAsync(user, authProperties, claims);

                ViewBag.nameSurname = user.NameSurname;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<string> getNameSurname()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user.NameSurname;
        }

        public IActionResult ChangePassword()
        {
            ViewBag.title = "Change Password Page";
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModelDTO changePasswordModelDTO)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var result = await _userManager.ChangePasswordAsync(user, changePasswordModelDTO.OldPassword,changePasswordModelDTO.NewPassword);
                if (result.Succeeded)
                {
                    ViewBag.error = "Password has been changed successfuly.";
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index","Home");
                }

                ViewBag.error = result.Errors;
            }

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string email, string password)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = "Berke Yıldırım",
                UserName = email,
                Email = email,
                PhoneNumber = "5555555555"
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            string errorshtml = "<ul>";
            foreach (var error in result.Errors)
            {
                errorshtml += $"<li>{error.Description}</li>";
            }
            
            errorshtml += "</ul>";
            TempData["error"] = errorshtml;

            return View();
        }
    }
}
