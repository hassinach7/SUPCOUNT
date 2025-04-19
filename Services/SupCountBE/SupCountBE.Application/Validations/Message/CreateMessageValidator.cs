using FluentValidation;
using SupCountBE.Application.Commands.Message;

namespace SupCountBE.Application.Validations.Message;

public class CreateMessageValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(500);

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender is required.");
    }
}
