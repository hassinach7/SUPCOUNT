using FluentValidation;
using SupCountBE.Application.Commands.Expense;

namespace SupCountBE.Application.Validations.Expense
{
    public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required.");

            RuleFor(x => x.PayerId)
                .NotEmpty()
                .WithMessage("Payer ID is required.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category ID is required.");

            RuleFor(x => x.GroupId)
                .GreaterThan(0)
                .WithMessage("Group ID is required.");

        }
       
    }
}
