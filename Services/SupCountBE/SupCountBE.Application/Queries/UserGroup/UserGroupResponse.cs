
namespace SupCountBE.Application.Queries.UserGroup
{
    public class UserGroupResponse
    {
        public string UserId { get; set; } = null!;
        public int GroupId { get; set; }
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
