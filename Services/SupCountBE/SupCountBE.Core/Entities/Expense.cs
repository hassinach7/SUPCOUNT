

namespace SupCountBE.Core.Entities;

public class Expense : BaseEntity
{
    public required string Title { get; set; }
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public required string PayerId { get; set; }
    public User? Payer { get; set; }
    public Category? Category { get; set; }
    public required int? CategoryId { get; set; }
    public Group? Group { get; set; }
    public required int? GroupId { get; set; }
    public ICollection<Participation>? Participations { get; set; }
    public ICollection<Justification>?  Justifications { get; set; }
}
