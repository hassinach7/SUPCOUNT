

namespace SupCountBE.Core.Entities
{
    public class UserGroup

    {
        public required string UserId { get; set; }
        public  required string GroupId { get; set; }

        public required  string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
        public Group? Group { get; set; }

    }
}
