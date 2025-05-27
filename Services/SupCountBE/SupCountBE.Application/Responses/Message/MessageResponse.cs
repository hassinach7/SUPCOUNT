
namespace SupCountBE.Application.Responses.Message
{
    public class MessageResponse: PrivateMessageResponse
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
    }
    public class PrivateMessageResponse
    {
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string SenderName { get; set; } = null!;
        public string RecipientName { get; set; } = null!;
    }
}