using EntityLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BusinessLayer.Services;
using Shared.Dtos.Auth;

namespace MatchEstate.Controllers;

[AllowAnonymous]
public class LoginController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<LoginDto> _loginDtoValidator;
    private readonly IValidator<ChangePasswordDto> _changePasswordDtoValidator;
    public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IValidator<LoginDto> loginDtoValidator, IValidator<ChangePasswordDto> changePasswordDtoValidator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _loginDtoValidator = loginDtoValidator;
        _changePasswordDtoValidator = changePasswordDtoValidator;
    }

    public IActionResult Index()
    {
        ViewBag.title = "Login Page";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] LoginDto dto)
    {
        var validateResult = await _loginDtoValidator.ValidateAsync(dto);
        if (validateResult.IsValid)
        {
            User? user = await _userManager.Users.Where(u => u.Email == dto.UsernameOrMail || u.UserName == dto.UsernameOrMail).FirstOrDefaultAsync();
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                ViewBag.error = "Username or password invalid.";
                return View();
            }

            var claimsIdentity = new ClaimsIdentity(authenticationType: "UserScheme");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _signInManager.SignInAsync(user, isPersistent: dto.RememberMe);

            var localeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
            user.LastActiveDate = localeNow;
            user.IsOnline = true;
            await _userManager.UpdateAsync(user);
            TempData["nameSurname"] = user.NameSurname;
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.error = ValidationMessageWriter.MessageWriter(validateResult.Errors.Select(x => x.ErrorMessage));
        }

        return View();
    }

    public async Task<IActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.IsOnline = false;
            var localeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
            user.LastActiveDate = localeNow;
            await _userManager.UpdateAsync(user);
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<string> getNameSurname()
    {
        var user = await _userManager.GetUserAsync(User);
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
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var validateResult = await _changePasswordDtoValidator.ValidateAsync(dto);
        if (validateResult.IsValid)
        {
            User? user = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (result.Succeeded)
            {
                TempData["success"] = "Password has been changed successfuly.";
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index","Home");
            }

            ViewBag.error = ValidationMessageWriter.MessageWriter(result.Errors.Select(x => x.Description));
        }
        else
        {
            ViewBag.error = ValidationMessageWriter.MessageWriter(validateResult.Errors.Select(err => err.ErrorMessage));
        }

        return View();
    }
}
