using SupCountBE.Application.Commands.User;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                throw new UserException($"User with ID {request.Id} not found.");
            }

            _mapper.Map(request, user);
            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }


}
