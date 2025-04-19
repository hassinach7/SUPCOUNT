namespace SupCountBE.Application.Responses.UserGroup;

public class UserGroupResponse
{
    public string UserId { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
