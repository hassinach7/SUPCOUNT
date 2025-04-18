using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupCountBE.Core.Entities;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.API.Middleware;

public static class CreateDefaultUserAndRole
{
    public static async Task InitializeUserAndRole(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SupCountDbContext>();
            dbContext.Database.Migrate();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var applicationRole = new ApplicationRole
                    {
                        Name = role
                    };
                    await roleManager.CreateAsync(applicationRole);
                }
            }
            if (await userManager.FindByEmailAsync("admin@supCount.com") == null)
            {
                var admin = new User { FullName = "Admin Admin", UserName = "admin@supCount.com", Email = "admin@supCount.com" };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}
