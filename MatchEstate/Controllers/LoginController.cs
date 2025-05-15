using Azure.Core;
using DTOLayer.Dtos;
using EntityLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MatchEstate.Controllers;

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
            User? user = await _userManager.Users.Where(u => u.Email == model.Mail || u.UserName == model.Mail).FirstOrDefaultAsync();
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

    [HttpPost]
    public async Task<IActionResult> Signup()
    {
        var user = new User()
        {
            Id = "ccab2c66-00da-4833-86b9-8c2278f75c6e",
            NameSurname = "Berke Yıldırım",
            Email = "berke.yildirimm44@gmail.com",
            UserName = "berkeyld44"
        };
        await _userManager.CreateAsync(user, "Berkeyld.44");

        return Ok();
    }
}
