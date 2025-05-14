using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Auth
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }


}

