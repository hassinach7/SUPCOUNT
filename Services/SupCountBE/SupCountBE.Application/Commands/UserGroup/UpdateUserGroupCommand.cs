using SupCountBE.Application.Responses.UserGroup;

namespace SupCountBE.Application.Commands.UserGroup;

public class UpdateUserGroupCommand : IRequest<UserGroupResponse>
{
    public string UserId { get; set; } = null!;
    public int GroupId { get; set; } 
    public string Role { get; set; } = null!;
}
