using FluentValidation;
using SupCountBE.Application.Commands.Group;

namespace SupCountBE.Application.Validations.Group;

public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(250)
            .WithMessage("Description must not exceed 250 characters.");
    }
}
