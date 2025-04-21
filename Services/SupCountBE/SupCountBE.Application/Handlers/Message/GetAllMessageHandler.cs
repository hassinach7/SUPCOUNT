
using SupCountBE.Application.Queries.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    internal class GetAllMessageHandler(IMessageRepository messageRepository, IMapper mapper) : IRequestHandler<GetAllMessageQuery, IList<MessageResponse>>
    {
        private readonly IMessageRepository _messageRepository = messageRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<MessageResponse>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAllListIncludingAsync(includeSender: true, includeRecipient: true, includeGroup: true);
            return _mapper.Map<IList<MessageResponse>>(messages);
        }
    }
    
    
}
