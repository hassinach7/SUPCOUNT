using SupCountBE.Application.Queries.User;
using SupCountBE.Application.Responses.User;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, IList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IList<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllListIncludingAsync(includeExpenses: true, includeGroups: true, includeReimbursements: true);
            return _mapper.Map<IList<UserResponse>>(users);
        }
    }
}
