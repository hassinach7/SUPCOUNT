using FluentValidation;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Validations.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message;

public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, MessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    public CreateMessageHandler(IMessageRepository messageRepository, IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _groupRepository = groupRepository;
        _userRepository = userRepository;

    }

    public async Task<MessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateMessageValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);
        if (request.GroupId != null)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId.Value);
            if (group == null)
                throw new Exception("Group not found.");
        }
        if (request.RecipientId != null)
        {
            int recipientId = int.Parse(request.RecipientId);
            var user = await _userRepository.GetByIdAsync(recipientId);
            if (user == null)
                throw new Exception("Recipient user not found.");
        }



        var message = new Core.Entities.Message
        {
            Content = request.Content,
            SenderId = request.SenderId,
            RecipientId = request.RecipientId,
            GroupId = request.GroupId
        };

        await _messageRepository.AddAsync(message);

        var full = await _messageRepository.GetByIdIncludingAsync(message.Id, includeSender: true, includeRecipient: true, includeGroup: true);
        return _mapper.Map<MessageResponse>(full);
    }
}
