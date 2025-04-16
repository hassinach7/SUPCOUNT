using Microsoft.AspNetCore.Identity;

namespace SupCountBE.Core.Entities;

public class ApplicationRole : IdentityRole
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
