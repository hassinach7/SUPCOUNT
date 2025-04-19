using FluentValidation;
using SupCountBE.Application.Commands.UserGroup;
using SupCountBE.Application.Responses.UserGroup;
using SupCountBE.Application.Validations.UserGroup;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.UserGroup;

public class UpdateUserGroupHandler : IRequestHandler<UpdateUserGroupCommand, UserGroupResponse>
{
    private readonly IUserGroupRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserGroupHandler(IUserGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserGroupResponse> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserGroupValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var userGroup = await _repository.GetByIdsIncludingAsync(
            request.UserId,
            request.GroupId,
            includeUser: false,
            includeGroup: true
        );

        if (userGroup is null)
            throw new Exception("UserGroup not found.");

        userGroup.Role = request.Role;

        await _repository.UpdateAsync(userGroup);

        return _mapper.Map<UserGroupResponse>(userGroup);
    }
}
