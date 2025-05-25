using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;

namespace SupCountBE.Infrastructure.Repositories;

public class ParticipationRepository : AsyncRepository<Participation>, IParticipationRepository
{
    public ParticipationRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<IList<Participation>> GetListIncludingAsync(ParticipationIncludingProperties participationIncludingProperties)
    {
        var query = Get(participationIncludingProperties);
        return await query.ToListAsync();
    }

    public async Task<Participation?> GetByIdsAsync(int expenseId)
    {
        return await _dbContext.Participations
            .FirstOrDefaultAsync(p => p.ExpenseId == expenseId);
    }

    public async Task<Participation?> GetByIdsIncludingAsync(int expenseId, ParticipationIncludingProperties participationIncludingProperties)
    {
        var query = Get(participationIncludingProperties);
        return await query.SingleOrDefaultAsync(p => p.ExpenseId == expenseId);
    }

    private IQueryable<Participation> Get(ParticipationIncludingProperties props)
    {
        var query = _dbContext.Participations.AsQueryable();

        if (props.IncludeUsers)
        {
            query = query.Include(p => p.User);
        }

        if (props.IncludeExpenses)
        {
            query = query.Include(p => p.Expense);
        }

        return query;
    }

    public async Task<IList<Participation>> GetListByUserIdAsync(string userId)
    {
        return await _dbContext.Participations.Include(o => o.Expense)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
}
