using System.ComponentModel.DataAnnotations;

namespace SupCountFE.MVC.ViewModels.Participation;

public class ParticipateExpenseVM
{

    [Required(ErrorMessage = "Expense is required.")]   
    public int? ExpenseId { get; set; }
    [Required(ErrorMessage = "Weight is required.")]
    [Range(1, float.MaxValue, ErrorMessage = $"Weight must be greather than  1.")]
    public float Weight { get; set; } 
    public string ExpenseTitle { get; set; } = null!;
}
