using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Category
{
    public class CreateCategoryVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
