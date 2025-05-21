using SupCountBE.Application.Queries.User;
using SupCountBE.Application.Responses.User;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByIdAsync(request.Id);
        if (user is null)
            return null;
        var mappedUser =  this._mapper.Map<UserResponse>(user);
        var roles = await this._userRepository.GetRolesByUserIdAsync(user.Id);
        mappedUser.Roles = roles;
        return mappedUser;
    }
}


