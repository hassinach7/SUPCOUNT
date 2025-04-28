using FluentValidation;
using SupCountBE.Application.Commands.Reimbursement;

namespace SupCountBE.Application.Validations.Reimbursement;

public class CreateReimbursementValidator : AbstractValidator<CreateReimbursementCommand>
{
    public CreateReimbursementValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        //RuleFor(x => x.SenderId).NotEmpty();
        RuleFor(x => x.BeneficiaryId)
             .NotNull()
             .WithMessage("Beneficiary ID is required."); 
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.GroupId)
         .NotNull()
         .WithMessage("Expense Id is required."); 
    }
}
