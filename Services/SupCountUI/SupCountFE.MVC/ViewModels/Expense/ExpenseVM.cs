namespace SupCountFE.MVC.ViewModels.Expense;

public class ExpenseVM
{

    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public ExpenseGroupVM Group { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string ParticipationCount { get; set; } = null!;
    public string JustificationCount { get; set; } = null!;
    public string Payer { get; set; } = null!;
    public IList<string> Members { get; set; } = new List<string>();
}
public class ExpenseGroupVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
