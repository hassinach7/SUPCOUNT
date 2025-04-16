using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class UserRepository : AsyncRepository<User>, IUserRepository
{
    public UserRepository(SupCountDbContext dbContext) : base(dbContext) { }

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
}
