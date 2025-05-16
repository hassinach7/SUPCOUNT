using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Expense
{
    public class UpdateExpenseVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public int? CategoryId { get; set; }
    }
}
