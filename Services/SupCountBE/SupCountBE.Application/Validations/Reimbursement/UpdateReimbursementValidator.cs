using FluentValidation;
using SupCountBE.Application.Commands.Reimbursement;

namespace SupCountBE.Application.Validations.Reimbursement;

public class UpdateReimbursementValidator : AbstractValidator<UpdateReimbursementCommand>
{
    public UpdateReimbursementValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");
    }
}

