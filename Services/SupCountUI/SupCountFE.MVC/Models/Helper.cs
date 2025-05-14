namespace SupCountFE.MVC.Models
{
    public class Helper
    {
        private readonly IHttpContextAccessor _httpContext;

        public Helper(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        // Properties to get user data from session
        public string? JWTToken => _httpContext.HttpContext!.Session.GetString("JWTToken");
        public string? Email => _httpContext.HttpContext!.Session.GetString("Email");
        public string? UserName => _httpContext.HttpContext!.Session.GetString("UserName");
        public string? UserId => _httpContext.HttpContext!.Session.GetString("UserId");
        public string[] UserRoles => (_httpContext.HttpContext!.Session.GetString("UserRoles") ?? "")
                                     .Split(',', StringSplitOptions.RemoveEmptyEntries);

        public DateTime? TokenExpiry
        {
            get
            {
                var expiryStr = _httpContext.HttpContext!.Session.GetString("TokenExpiry");
                return DateTime.TryParse(expiryStr, out var expiry) ? expiry : null;
            }
        }

        public bool IsSessionExpired => TokenExpiry.HasValue && TokenExpiry.Value < DateTime.UtcNow;

        public bool IsAuthenticated => !string.IsNullOrEmpty(JWTToken) && !IsSessionExpired;
    }
}
