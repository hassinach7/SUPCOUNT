using SupCountBE.Application.Responses.Message;

namespace SupCountBE.Application.Commands.Message;

public class UpdateMessageCommand : IRequest<MessageResponse>
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
}
