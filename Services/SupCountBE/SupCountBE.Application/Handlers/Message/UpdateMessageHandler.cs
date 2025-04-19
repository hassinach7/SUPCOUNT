using FluentValidation;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Application.Validations.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message;

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
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var message = await _messageRepository.GetByIdAsync(request.Id);
        if (message is null)
            throw new Exception("Message not found.");

        message.Content = request.Content;

        await _messageRepository.UpdateAsync(message);

        var full = await _messageRepository.GetByIdIncludingAsync(message.Id, includeSender: true, includeRecipient: true, includeGroup: true);
        return _mapper.Map<MessageResponse>(full);
    }
}
