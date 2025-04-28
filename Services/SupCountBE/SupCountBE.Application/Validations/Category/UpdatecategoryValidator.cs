using FluentValidation;
using SupCountBE.Application.Commands.Category;

namespace SupCountBE.Application.Validations.Category;

public class UpdatecategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdatecategoryValidator()
    {
        RuleFor(x => x.Id)
               .NotNull()
               .WithMessage("ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");
    }
}


