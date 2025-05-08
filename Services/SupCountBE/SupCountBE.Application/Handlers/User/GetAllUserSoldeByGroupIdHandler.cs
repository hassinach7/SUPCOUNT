using SupCountBE.Application.Queries.User;
using SupCountBE.Application.Responses.User;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User;

public class GetAllUserSoldeByGroupIdHandler : IRequestHandler<GetAllUserSoldeByGroupIdQuery, IList<SoldeUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public GetAllUserSoldeByGroupIdHandler(IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<IList<SoldeUserResponse>> Handle(GetAllUserSoldeByGroupIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null)
        {
            throw new GroupException($"Group with ID {request.GroupId} not found.");
        }

        var users = await _userRepository.GetAllUsersByGroupIdAsync(request.GroupId);
        return users.Select(user => new SoldeUserResponse
        {
            UserId = user.Id,
            UserFullName = user.FullName,
            UserEmail = user.Email!,
            UserSolde = user.Balance
        }).ToList();
    }
}
