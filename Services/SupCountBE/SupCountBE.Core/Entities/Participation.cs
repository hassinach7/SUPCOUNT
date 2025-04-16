

namespace SupCountBE.Core.Entities;

public  class Participation 
{
    public float Weight { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public required string UserId { get; set; }
    public User? User { get; set; }
    public required int ExpenseId { get; set; }
    public Expense? Expense { get; set; }
}
