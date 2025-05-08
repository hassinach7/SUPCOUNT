using SupCountBE.Application.Dtos;

namespace SupCountBE.Application.Commands.Security
{
    public class GoogleAuthCommand : IRequest<LoginAuthResponseDto>
    {
        public required string Email { get; set; }
        public string? FullName { get; set; }
    }
   
}
