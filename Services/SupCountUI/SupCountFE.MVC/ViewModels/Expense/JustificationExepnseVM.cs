namespace SupCountFE.MVC.ViewModels.Expense;

public class JustificationExepnseVM
{
    public int Id { get; set; }
    public string ExpenseTitle { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string FileContent { get; set; } = null!;
}
