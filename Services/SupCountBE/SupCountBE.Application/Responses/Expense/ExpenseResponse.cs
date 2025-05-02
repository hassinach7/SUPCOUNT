
namespace SupCountBE.Application.Responses.Expense;

public class ExpenseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public ExpenseGroupResponse Group { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string ParticipationCount { get; set; } = null!;
    public string JustificationCount { get; set; } = null!;
    public string Payer { get; set; } = null!;
    public IList<string> Members { get; set; } = new List<string>();
}
public class ExpenseGroupResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
