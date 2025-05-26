using SupCountBE.Application.Queries.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    public class GetPrivateMessageQueryHandler : IRequestHandler<GetPrivateMessageQuery, IList<MessageResponse>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
      

        public GetPrivateMessageQueryHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<IList<MessageResponse>> Handle(GetPrivateMessageQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _messageRepository.GetCurrentUser();

            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (request.SenderId != currentUser && request.RecipientId != currentUser)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this private conversation.");
            }

            var messages = await _messageRepository.GetPrivateMessagesAsync(request.SenderId, request.RecipientId);

            var privateMessages = messages
                .Where(m => m.IsPrivate)
                .ToList();

            return _mapper.Map<IList<MessageResponse>>(privateMessages);
        }



    }
}
