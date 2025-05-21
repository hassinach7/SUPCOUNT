using SupCountBE.Application.Responses.User;

namespace SupCountBE.Application.Queries.User;

 public class GetUserByIdQuery : IRequest<UserResponse?>
{
    public string Id { get; set; }

    public GetUserByIdQuery(string id)
    {
        this.Id = id;
    }
}
