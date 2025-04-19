using FluentValidation;
using SupCountBE.Application.Commands.Justification;

namespace SupCountBE.Application.Validations.Justification;

public class CreateJustificationValidator : AbstractValidator<CreateJustificationCommand>
{
    public CreateJustificationValidator()
    {
        RuleFor(x => x.ExpenseId);
            

        RuleFor(x => x.FileContent)
            .NotNull()
            .WithMessage("File content is required.")
            .Must(f => f.Length > 0)
            .WithMessage("File must not be empty.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Invalid justification type.");
    }
}
