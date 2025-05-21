using FluentValidation;
using SupCountBE.Application.Commands.User;

namespace SupCountBE.Application.Validations.User;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
             .NotNull()
             .WithMessage("ID is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required");
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required")
            .MinimumLength(3)
            .WithMessage("Full name must be at least 3 characters long");
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long");
        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("Roles are required")
            .Must(roles => roles.Count > 0)
            .WithMessage("At least one role is required");
    }
}


