
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
using MatchEstate.Wrappers;
using BusinessLayer.Concrete;

namespace MatchEstate.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IValidator<LoginModelDTO> _validator;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IValidator<LoginModelDTO> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validator = validator;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Login Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginModelDTO model)
        {
            var validateResult = await _validator.ValidateAsync(model);
            if (validateResult.IsValid)
            {
                User? user = await _userManager.FindByNameAsync(model.Mail);
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ViewBag.loginFail = "Email or password invalid.";
                    return View();
                }

                var claimsIdentity = new ClaimsIdentity(authenticationType: "UserScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);

                TempData["nameSurname"] = user.NameSurname;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = validateResult.Errors.Select(x => x.ErrorMessage);
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

        [Authorize]
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
    }
}
