using Microsoft.AspNetCore.Identity;
using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class UserRepository : AsyncRepository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(SupCountDbContext dbContext, UserManager<User> userManager) : base(dbContext)
    {
        this._userManager = userManager;
    }

    public async Task<(bool, string)> CreateAsync(User user, string password, IList<string> roles)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            var rolesToAdd = (roles is null || roles.Count == 0) ? new List<string>() { "User" } : roles;
            var addToRoleResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                return (false, errors);
            }

            return (true, string.Empty);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }
    }

    public Task<IList<User>> GetAllListIncludingAsync(IncludingItem including)

    {
        var query = _dbContext.Users.AsQueryable();

        if (including.IncludeExpenses)
            query = query.Include(u => u.Expenses);

        if (including.IncludeReimbursements)
        {
            query = query
                .Include(u => u.ReimbursementsSent)
                .Include(u => u.ReimbursementsReceived);
        }

        if (including.IncludeGroups)
            query = query.Include(u => u.UserGroups!).ThenInclude(ug => ug.Group);

        return Task.FromResult(query.ToList() as IList<User>);
    }

    public async Task<IList<User>> GetAllUsersByGroupIdAsync(int groupId)
    {
        return await _dbContext.Users
             .Include(u => u.UserGroups)
             .Where(u => u.UserGroups!.Any(ug => ug.GroupId == groupId))
             .ToListAsync();
    }

    public async Task<User?> GetByIdIncludingAsync(string id, IncludingItem including)
    {
        var query = _dbContext.Users.AsQueryable();

        if (including.IncludeExpenses) query = query.Include(u => u.Expenses);
        if (including.IncludeParticipations) query = query.Include(u => u.Participations);
        if (including.IncludeReimbursementsSent) query = query.Include(u => u.ReimbursementsSent);
        if (including.IncludeReimbursementsReceived) query = query.Include(u => u.ReimbursementsReceived);
        if (including.IncludeSentMessages) query = query.Include(u => u.SentMessages);
        if (including.IncludeReceivedMessages) query = query.Include(u => u.ReceivedMessages);
        if (including.IncludeUserGroups) query = query.Include(u => u.UserGroups);

        return await query.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetReciepientByIdAsync(string RecipientId)
    {
        return await _userManager.FindByIdAsync(RecipientId);
    }

    public async Task<IList<string>> GetRolesByUserIdAsync(string userId)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new Exception($"User with ID {userId} not found.");
        }
        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task UpdateAsync(User user, List<string> roles)
    {
        await UpdateAsync(user);
        if (roles.Count > 0)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = roles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(roles).ToList();
            if (rolesToAdd.Count > 0)
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }
        }
    }
}
