using SupCountBE.Application.Commands.User;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return false;
            }

            _mapper.Map(request, user);
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
    
    
}
