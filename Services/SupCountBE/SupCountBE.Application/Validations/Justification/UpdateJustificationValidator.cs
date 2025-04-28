using FluentValidation;
using SupCountBE.Application.Commands.Justification;

namespace SupCountBE.Application.Validations.Justification;

public class UpdateJustificationValidator : AbstractValidator<UpdateJustificationCommand>
{
    public UpdateJustificationValidator()
    {
        RuleFor(x => x.Id)
             .NotNull()
             .WithMessage("ID is required."); ;
           

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
