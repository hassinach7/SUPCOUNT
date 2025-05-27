namespace SupCountBE.Application.Commands.Message;

public class CreatePrivateMessageCommand: IRequest<Unit>
{
    public string Content { get; set; } = null!;
    public string RecipientId { get; set; } = null!;
}
