using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using SupCountFE.MVC.Services.Contracts;
using System.Security.Claims;

namespace SupCountFE.MVC.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
       _authService = authService;
    }
    [HttpGet("login")]
    public IActionResult Login()
    {
               var redirectUrl = Url.Action("googleResponse", "account");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);

    }

    [HttpGet("googleResponse")]

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

        //Optionnel: enregistrez l'utilisateur côté Web API ici
        //     display all data claims
        Console.WriteLine(claims.Count());
        foreach (var claim in claims)
        {
            Console.WriteLine($"Controller : Type: {claim.Type}, Value: {claim.Value}");
        }


        //var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        //var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //var userId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //await _authService.LoginAsync(new LoginVM
        //{
        //    GoogleAuth = true,
        //    UserName = email ?? string.Empty,
        //    Password = "123"
        //});
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}

