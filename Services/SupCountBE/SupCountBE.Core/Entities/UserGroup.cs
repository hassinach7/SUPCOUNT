﻿

namespace SupCountBE.Core.Entities;

public class UserGroup 
{
    //public int Id { get; set; }
    public required string UserId { get; set; }
    public  required int GroupId { get; set; }
    public required  string Role { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public Group? Group { get; set; }
}
