using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Group
{
    public class UpdateGroupVM
    {
        [Required]
        public int Id { get; set; } 

        [Required(ErrorMessage ="The group Name is required")]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

    }



}
