using SupCountFE.MVC.Services.Contracts;

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
    public IActionResult Login()
    {
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
                HttpContext.Session.SetString("JWTToken", result.Token!);
                HttpContext.Session.SetString("Email", result.Email!);
                HttpContext.Session.SetString("UserName", result.UserName!);
                HttpContext.Session.SetString("UserId", result.UserId!);
                HttpContext.Session.SetString("UserRoles", string.Join(",", result.Roles!));
                HttpContext.Session.SetString("TokenExpiry", result.ExpiresOn.ToString());

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result!.Messages[0].Replace("[\"", "").Replace("\"]", ""));
            return View(model);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An error occurred while processing your request.");
            return View(model);
        }
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
