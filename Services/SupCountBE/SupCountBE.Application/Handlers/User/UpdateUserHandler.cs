using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Validations.User;
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
            UpdateUserValidator validator = new UpdateUserValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new UserException(validationResult.Errors.First().ErrorMessage);
            }
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                throw new UserException($"User with ID {request.Id} not found.");
            }

            _mapper.Map(request, user);
            await _userRepository.UpdateAsync(user, request.Roles);

            return Unit.Value;
        }
    }


}
