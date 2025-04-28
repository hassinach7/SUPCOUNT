namespace SupCountBE.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;

    }
    
}
