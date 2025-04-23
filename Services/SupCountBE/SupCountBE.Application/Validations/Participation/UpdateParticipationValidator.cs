using FluentValidation;
using SupCountBE.Application.Commands.Participation;

namespace SupCountBE.Application.Validations.Participation;

public class UpdateParticipationValidator : AbstractValidator<UpdateParticipationCommand>
{
    public UpdateParticipationValidator()
    {
        RuleFor(x => x.UserId)
         .NotNull()
         .WithMessage("User ID is required.");

        RuleFor(x => x.ExpenseId)
         .NotNull()
         .WithMessage("Expense Id is required.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");
    }
}
