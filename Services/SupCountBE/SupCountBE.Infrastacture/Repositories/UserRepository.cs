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

    public async Task<(bool, string)> CreateAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return (true,string.Empty);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }
    }

    public Task<IList<User>> GetAllListIncludingAsync(bool includeExpenses = false, bool includeReimbursements = false, bool includeGroups = false)

    {
        var query = _dbContext.Users.AsQueryable();

        if (includeExpenses)
            query = query.Include(u => u.Expenses);

        if (includeReimbursements)
        {
            query = query
                .Include(u => u.ReimbursementsSent)
                .Include(u => u.ReimbursementsReceived);
        }

        if (includeGroups)
            query = query.Include(u => u.UserGroups!).ThenInclude(ug => ug.Group);

        return Task.FromResult(query.ToList() as IList<User>);
    }

    public async Task<User?> GetByIdIncludingAsync(
        string id,
        bool includeExpenses = false,
        bool includeParticipations = false,
        bool includeReimbursementsSent = false,
        bool includeReimbursementsReceived = false,
        bool includeSentMessages = false,
        bool includeReceivedMessages = false,
        bool includeUserGroups = false)
    {
        var query = _dbContext.Users.AsQueryable();

        if (includeExpenses) query = query.Include(u => u.Expenses);
        if (includeParticipations) query = query.Include(u => u.Participations);
        if (includeReimbursementsSent) query = query.Include(u => u.ReimbursementsSent);
        if (includeReimbursementsReceived) query = query.Include(u => u.ReimbursementsReceived);
        if (includeSentMessages) query = query.Include(u => u.SentMessages);
        if (includeReceivedMessages) query = query.Include(u => u.ReceivedMessages);
        if (includeUserGroups) query = query.Include(u => u.UserGroups);

        return await query.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}
