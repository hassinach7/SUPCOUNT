namespace SupCountFE.MVC.Models
{
    public class AuthModel
    {
        public List<string> Messages { get; set; } = new();
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public List<string>? Roles { get; set; }
    }
}
