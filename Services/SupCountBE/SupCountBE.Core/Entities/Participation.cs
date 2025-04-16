

namespace SupCountBE.Core.Entities;

public  class Participation : BaseEntity
{
    public float Weight { get; set; }

    public required string UserId { get; set; }
    public User? User { get; set; }
    public required int ExpenseId { get; set; }
    public Expense? Expense { get; set; }
}
