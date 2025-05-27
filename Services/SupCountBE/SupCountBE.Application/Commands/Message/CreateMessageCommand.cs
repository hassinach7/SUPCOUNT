using SupCountBE.Application.Responses.Message;

namespace SupCountBE.Application.Commands.Message;

public class CreateMessageCommand : IRequest<MessageResponse>
{
    public string Content { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public string? RecipientId { get; set; }
    public int? GroupId { get; set; }
    public bool? IsPrivate { get; set; } = false;
}
