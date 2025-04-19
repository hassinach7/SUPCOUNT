

namespace SupCountBE.Application.Responses.Message
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public string SenderName { get; set; } = null!;
        public string? RecipientName { get; set; }
        public string? GroupName { get; set; }
    }

}
