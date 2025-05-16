using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Expense;

public class CreateExpenseVM
{
    [Required]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string? Title { get; set; } = null!;

    [Range(1, float.MaxValue, ErrorMessage = "Amount must be at least 1.")]
    public float Amount { get; set; }
    [Required]
    public DateTime? Date { get; set; }

    [Required]
    public int? GroupId { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    public SelectList? GroupsItems { get; set; }
    public SelectList? CategoriesItems { get; set; }
    public IList<IFormFile>? Justifications { get; set; }
}
