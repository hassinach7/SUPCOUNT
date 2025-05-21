using Microsoft.AspNetCore.Identity;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Infrastacture.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> roleManager;
  

    public RoleRepository(RoleManager<ApplicationRole> roleManager)
    {
        this.roleManager = roleManager;
    }
    public async Task<List<string?>> GetListAsync()
    {
        return await roleManager.Roles.Select(x => x.Name).ToListAsync();
    }
}
