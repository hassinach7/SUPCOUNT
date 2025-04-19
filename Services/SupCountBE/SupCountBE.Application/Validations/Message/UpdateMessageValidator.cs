using FluentValidation;
using SupCountBE.Application.Commands.Message;

namespace SupCountBE.Application.Validations.Message;

public class UpdateMessageValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageValidator()
    {
        RuleFor(x => x.Id);
           
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(500);
    }
}
