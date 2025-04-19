using FluentValidation;
using SupCountBE.Application.Commands.Transaction;

namespace SupCountBE.Application.Validations.Transaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.ReimbursementId).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}
