using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using System.Security.Claims;

namespace SupCountFE.MVC.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // GET: /Auth/Login
    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (!string.IsNullOrEmpty(email))
        {
            var result = await _authService.LoginAsync(new LoginVM
            {
                GoogleAuth = true,
                UserName = email,
                Password = new Random().Next(100000, 999999).ToString()
            });

            if (result != null && result.IsAuthenticated)
            {
                SignInUser(result);

                // Remove email from claims
                var claimsIdentity = User.Identity as ClaimsIdentity;
                claimsIdentity?.RemoveClaim(claimsIdentity.FindFirst(ClaimTypes.Email));

                return RedirectToAction("Index", "Home");
            }
        }
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model);

            if (result != null && result.IsAuthenticated)
            {
                SignInUser(result);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result!.Messages[0].Replace("[\"", "").Replace("\"]", ""));
            return View(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ModelState.AddModelError("", "An error occurred while processing your request. " + ex.Message);
            return View(model);
        }
    }

    private void SignInUser(AuthModel result)
    {
        HttpContext.Session.SetString("JWTToken", result.Token!);
        HttpContext.Session.SetString("Email", result.Email!);
        HttpContext.Session.SetString("UserName", result.UserName!);
        HttpContext.Session.SetString("UserId", result.UserId!);
        HttpContext.Session.SetString("UserRoles", string.Join(",", result.Roles!));
        HttpContext.Session.SetString("TokenExpiry", result.ExpiresOn.ToString());
    }

    [HttpPost]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();


        return RedirectToAction("Login", "Auth");
    }

    // GET: /Auth/ExternalLogin?provider=Google
    [HttpGet]
    public IActionResult ExternalLogin(string provider = "Google")
    {
        return Redirect($"https://localhost:7280/api/Auth/ExternalLogin?provider={provider}");
    }

}
