using SupCountBE.Application.Queries.User;
using SupCountBE.Application.Responses.User;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<UserResponse?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
            return null;
        return _mapper.Map<UserResponse>(user);
    }
}
