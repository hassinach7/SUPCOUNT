using FluentValidation;
using SupCountBE.Application.Commands.UserGroup;

namespace SupCountBE.Application.Validations.UserGroup;

public class CreateUserGroupValidator : AbstractValidator<CreateUserGroupCommand>
{
    public CreateUserGroupValidator()
    {
        //RuleFor(x => x.UserId)
        //    .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.GroupId)
             .NotNull()
             .WithMessage("Group Id is required.");
            

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");
    }
}
