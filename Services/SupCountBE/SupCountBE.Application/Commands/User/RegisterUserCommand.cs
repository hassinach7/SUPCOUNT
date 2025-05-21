namespace SupCountBE.Application.Commands.User;

public class RegisterUserCommand: IRequest<string>
{
    public string Email { get; set; } =null!;
    public string Password { get; set; } =null!;
    public string PhoneNumber { get; set; } =null!;
    public string FullName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public List<string> Roles { get; set; } = new List<string>();
}
