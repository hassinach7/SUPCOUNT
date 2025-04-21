using FluentValidation;
using SupCountBE.Application.Commands.Expense;

namespace SupCountBE.Application.Validations.Expense
{
    public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseCommand>
    {
        public UpdateExpenseValidator()
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

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("ID is required.");

            RuleFor(x => x.CategoryId)
                .NotNull()
                .WithMessage("Category ID is required.");

            RuleFor(x => x.GroupId)
                .NotNull()
                .WithMessage("Group ID is required.");
        }
    }
}
