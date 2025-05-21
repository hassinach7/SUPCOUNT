namespace SupCountBE.Application.Commands.User;

public class UpdateUserCommand : IRequest<Unit>
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public List<string> Roles { get; set; } = new List<string>();
}
