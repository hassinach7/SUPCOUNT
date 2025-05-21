using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.User
{
    public class UpdateUserVM
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();
        public IList<string> SelectedRoles { get; set; } = new List<string>();

    }
}
