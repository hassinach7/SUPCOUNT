namespace SupCountBE.Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatdAt { get; set; }
}
