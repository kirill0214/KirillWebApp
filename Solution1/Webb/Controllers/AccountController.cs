using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // Запуск входа через Google
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("GoogleResponse");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return Challenge(properties, "Google");
    }

    // Обработка ответа от Google
    public async Task<IActionResult> GoogleResponse()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("Index", "Home");

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user);
            await _userManager.AddLoginAsync(user, info);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }

    // Выход
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}


