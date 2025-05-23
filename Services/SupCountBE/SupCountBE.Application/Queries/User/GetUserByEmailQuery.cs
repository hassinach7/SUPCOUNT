using SupCountBE.Application.Responses.User;

namespace SupCountBE.Application.Queries.User;

public class GetUserByEmailQuery : IRequest<UserResponse?>
{
    public string Email { get; }
    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}
