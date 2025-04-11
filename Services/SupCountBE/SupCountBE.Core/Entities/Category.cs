
namespace SupCountBE.Core.Entities
{
    public class Category

    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
    }
}
