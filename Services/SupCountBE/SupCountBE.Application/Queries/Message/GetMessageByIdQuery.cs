using SupCountBE.Application.Responses.Message;

namespace SupCountBE.Application.Queries.Message
{
    public class GetMessageByIdQuery(int id) : IRequest<MessageResponse>
    {
        public int Id = id;
    }
}
