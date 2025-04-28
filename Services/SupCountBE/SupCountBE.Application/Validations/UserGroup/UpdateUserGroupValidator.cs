using FluentValidation;
using SupCountBE.Application.Commands.UserGroup;

namespace SupCountBE.Application.Validations.UserGroup;

public class UpdateUserGroupValidator : AbstractValidator<UpdateUserGroupCommand>
{
    public UpdateUserGroupValidator()
    {
        //RuleFor(x => x.UserId)
        //    .NotNull().WithMessage("User ID is required.");

        RuleFor(x => x.GroupId)
          .NotNull()
          .WithMessage("Group Id is required.");

        RuleFor(x => x.Role)
            .NotNull().WithMessage("Role is required.")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");
    }
}
