using SupCountBE.Application.Queries.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    public class GetAllMessageHandler : IRequestHandler<GetAllMessageQuery, IList<MessageResponse>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetAllMessageHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<IList<MessageResponse>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAllListIncludingAsync(new MessageIncludingProperties
            {
                IncludeSenders = true,
                IncludeRecipients = true,
                IncludeGroups = true
            });

            return _mapper.Map<IList<MessageResponse>>(messages);
        }
    }
}


