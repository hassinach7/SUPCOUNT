using SupCountBE.Application.Commands.Security;
using SupCountBE.Application.Common.Models;
using SupCountBE.Application.Common.Services;
using SupCountBE.Application.Dtos;

namespace SupCountBE.Application.Handlers.Security;

public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, LoginAuthResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginAuthCommandHandler(ITokenGenerator tokenGenerator, IMapper mapper)
    {
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
    }

    public async Task<LoginAuthResponseDto> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
    {
        var authModel = await _tokenGenerator.GetTokenAsync(new TokenRequestModel { Email = request.UserName, Password = request.Password, GoogleAuth =request.GoogleAuth});
        return _mapper.Map<LoginAuthResponseDto>(authModel); 
    }
}
