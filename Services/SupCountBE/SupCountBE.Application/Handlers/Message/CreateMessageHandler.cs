using FluentValidation;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Validations.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, MessageResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public CreateMessageHandler(IMessageRepository messageRepository, IUserRepository userRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<MessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMessageValidator();
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            // Validation logique RecipientId vs GroupId
            if ((request.RecipientId == null && request.GroupId == null) ||
                (request.RecipientId != null && request.GroupId != null))
            {
                throw new Exception("You must provide either a RecipientId or a GroupId, but not both.");
            }

            // Si RecipientId, on vérifie que l'utilisateur existe
            if (request.RecipientId != null)
            {
                var recipient = await _userRepository.GetReciepientByIdAsync(request.RecipientId);
                if (recipient == null)
                    throw new Exception("Recipient not found.");
            }

            // Si GroupId, on vérifie que le groupe existe
            if (request.GroupId != null)
            {
                var group = await _groupRepository.GetByIdAsync(request.GroupId.Value);
                if (group == null)
                    throw new Exception("Group not found.");
            }

            var message = new Core.Entities.Message
            {
                Content = request.Content,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                GroupId = request.GroupId
            };

            await _messageRepository.AddAsync(message);

            var createdMessage = await _messageRepository.GetByIdIncludingAsync(message.Id, includeSender: true, includeRecipient: true, includeGroup: true);
            return _mapper.Map<MessageResponse>(createdMessage);
        }
    }
}
