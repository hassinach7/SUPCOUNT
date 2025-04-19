using FluentValidation;
using SupCountBE.Application.Commands.Reimbursement;

namespace SupCountBE.Application.Validations.Reimbursement;

public class UpdateReimbursementValidator : AbstractValidator<UpdateReimbursementCommand>
{
    public UpdateReimbursementValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Valid reimbursement ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");
    }
}

