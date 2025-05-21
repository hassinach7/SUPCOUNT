using FluentValidation;
using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Validations.User;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.User;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validation request
      
        var validator = new RegisterUserValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        // Check user exist 
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
            throw new UserException("User with this email already exists.");

        var user = _mapper.Map<Core.Entities.User>(request);
        var result = await _userRepository.CreateAsync(user, request.Password, request.Roles);
        if (!result.Item1)
            throw new UserException(result.Item2);

        return user.Id;  
    }
}
