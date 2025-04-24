using SupCountBE.Application.Responses.UserGroup;

namespace SupCountBE.Application.Queries.UserGroup
{
    public class GetUserGroupByIdQuery(int groupId) : IRequest<UserGroupResponse>
    {
        public int GroupId { get; set; } = groupId ;
    }
}
