using SupCountBE.Application.Responses.Group;

namespace SupCountBE.Application.Queries.Group
{
    public class GetGroupByIdQuery(int id) : IRequest<GroupResponse>
    {
        public int Id { get; set; } = id;
    }
}
