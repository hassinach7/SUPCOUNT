using SupCountBE.Application.Commands.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message;

public class CreatePrivateMessageHandler : IRequestHandler<CreatePrivateMessageCommand, Unit>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public CreatePrivateMessageHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(CreatePrivateMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _messageRepository.GetCurrentUser();
        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var recipient = await _userRepository.GetByIdIncludingAsync(request.RecipientId, new());
        if (recipient == null)
        {
            throw new ArgumentException($"Recipient with ID {request.RecipientId} does not exist.");
        }
        var message = new Core.Entities.Message
        {
            Content = request.Content,
            SenderId = currentUser,
            RecipientId = recipient.Id,
            IsPrivate = true
        };
        await _messageRepository.AddAsync(message);
        return Unit.Value;
    }
}
