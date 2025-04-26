namespace SupCountBE.Application.Commands.User;

public class RegisterUserCommand: IRequest<Unit>
{
    public string Email { get; set; } =null!;
    public string Password { get; set; } =null!;
    public string PhoneNumber { get; set; } =null!;
    public string FullName { get; set; } = null!;
}
