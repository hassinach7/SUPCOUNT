using SupCountBE.Application.Queries.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message;

public class GetPrivateMessageQueryHandler : IRequestHandler<GetPrivateMessageQuery, IList<PrivateMessageResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
  

    public GetPrivateMessageQueryHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<IList<PrivateMessageResponse>> Handle(GetPrivateMessageQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _messageRepository.GetCurrentUser();

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var messages = await _messageRepository.GetPrivateMessagesAsync(currentUser);

        return _mapper.Map<IList<PrivateMessageResponse>>(messages);
    }



}
