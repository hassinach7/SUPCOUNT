using SupCountBE.Application.Responses.Message;
namespace SupCountBE.Application.Queries.Message;

public class GetPrivateMessageQuery : IRequest<IList<MessageResponse>>
{
    public GetPrivateMessageQuery(string senderId, string recipientId)
    {
        SenderId = senderId;
        RecipientId = recipientId;
    }

    public string SenderId { get; set; } = null!;
    public string RecipientId { get; set; } = null!;  
}

