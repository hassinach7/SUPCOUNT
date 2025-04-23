using FluentValidation;
using SupCountBE.Application.Commands.Participation;

namespace SupCountBE.Application.Validations.Participation;

public class CreateParticipationValidator : AbstractValidator<CreateParticipationCommand>
{
    public CreateParticipationValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        //RuleFor(x => x.UserId)
        //    .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.ExpenseId)
            .GreaterThan(0).WithMessage("Expense ID is required.");
    }
}
