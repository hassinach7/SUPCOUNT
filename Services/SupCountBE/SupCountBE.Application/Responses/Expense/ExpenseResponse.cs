
namespace SupCountBE.Application.Responses.Expense;

public class ExpenseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public string GroupName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string ParticipationCount { get; set; } = null!;
    public string JustificationCount { get; set; } = null!;
}
