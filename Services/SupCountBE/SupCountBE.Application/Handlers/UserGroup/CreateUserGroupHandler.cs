using FluentValidation;
using SupCountBE.Application.Commands.UserGroup;
using SupCountBE.Application.Responses.UserGroup;
using SupCountBE.Application.Validations.UserGroup;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.UserGroup;

public class CreateUserGroupHandler : IRequestHandler<CreateUserGroupCommand, UserGroupResponse>
{
    private readonly IUserGroupRepository _userGrouprepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public CreateUserGroupHandler(IUserGroupRepository userGrouprepository, IMapper mapper, IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _userGrouprepository = userGrouprepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<UserGroupResponse> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserGroupValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

       

        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null)
            throw new Exception("Group not found.");


        var userGroup = new Core.Entities.UserGroup
        {
            UserId = _userGrouprepository.GetCurrentUser(),
            GroupId = request.GroupId,
            Role = request.Role
        }; 

        await _userGrouprepository.AddAsync(userGroup);

        var full = await _userGrouprepository.GetByIdsIncludingAsync(
          
            request.GroupId,
            includeUser: false,
            includeGroup: true
        );

        return _mapper.Map<UserGroupResponse>(full);
    }
}
