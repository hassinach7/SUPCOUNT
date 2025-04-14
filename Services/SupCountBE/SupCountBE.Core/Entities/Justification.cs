

using SupCountBE.Core.Enums;

namespace SupCountBE.Core.Entities;

public  class Justification : BaseEntity

{
    public int ExpenseId { get; set; }
    public Expense?  Expense { get; set; }
    public required byte[] FileContent { get; set; }
    public required JustificationTypeEnum Type { get; set; }
}
