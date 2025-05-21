using FluentValidation;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Validations.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, MessageResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public UpdateMessageHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageResponse> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMessageValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var message = await _messageRepository.GetByIdAsync(request.Id);
            if (message == null)
                throw new Exception("Message not found.");

            message.Content = request.Content;

            await _messageRepository.UpdateAsync(message);

            var updatedMessage = await _messageRepository.GetByIdIncludingAsync(
                message.Id,
                new MessageIncludingProperties
                {
                    IncludeSenders = true,
                    IncludeRecipients = true,
                    IncludeGroups = true
                });

            return _mapper.Map<MessageResponse>(updatedMessage);
        }
    }
}
