using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
