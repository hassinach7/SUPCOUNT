

namespace SupCountBE.Application.Responses.Message
{
    public  class MessageResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? SenderId { get; set; }
        public string? RecipientId { get; set; }
        public int? GroupId { get; set; }
    }
}
