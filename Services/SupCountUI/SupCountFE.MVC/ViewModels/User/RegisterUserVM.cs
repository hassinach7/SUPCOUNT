using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.User
{
    public class RegisterUserVM
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();

        [Display(Name = "Select Roles")]
        public IList<string> SelectedRoles { get; set; } = new List<string>();
    }
}
