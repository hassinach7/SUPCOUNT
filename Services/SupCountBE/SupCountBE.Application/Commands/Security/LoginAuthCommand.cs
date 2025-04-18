using SupCountBE.Application.Dtos;

namespace SupCountBE.Application.Commands.Security;

public class LoginAuthCommand: IRequest<LoginAuthResponseDto>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
