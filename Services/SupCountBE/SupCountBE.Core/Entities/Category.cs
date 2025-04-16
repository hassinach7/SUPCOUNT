
namespace SupCountBE.Core.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
}
