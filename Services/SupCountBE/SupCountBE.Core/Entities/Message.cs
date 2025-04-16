

namespace SupCountBE.Core.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }
    public int? GroupId { get; set; }
    public Group? Group { get; set; }
    public required string SenderId { get; set; }
    public string? RecipientId { get; set; }
    public User? Sender { get; set; }
    public User? Recipient { get; set; }
   
   
}
