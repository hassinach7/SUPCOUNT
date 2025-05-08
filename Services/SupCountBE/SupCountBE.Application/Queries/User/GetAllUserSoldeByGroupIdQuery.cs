using SupCountBE.Application.Responses.User;

namespace SupCountBE.Application.Queries.User;

public class GetAllUserSoldeByGroupIdQuery :IRequest<IList<SoldeUserResponse>>
{
    public int GroupId { get; set; }
}
