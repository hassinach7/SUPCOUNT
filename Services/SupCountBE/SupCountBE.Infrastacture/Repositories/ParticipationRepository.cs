using Microsoft.EntityFrameworkCore;
using SupCountBE.Core.Entities;
using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class ParticipationRepository : AsyncRepository<Participation>, IParticipationRepository
{
    public ParticipationRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<IList<Participation>> GetListIncludingAsync(
        bool includeUser = false,
        bool includeExpense = false)
    {
        var query = _dbContext.Participations.AsQueryable();

        if (includeUser)
        {
            query = query.Include(p => p.User);
        }

        if (includeExpense)
        {
            query = query.Include(p => p.Expense);
        }

        return await query.ToListAsync();
    }

    public async Task<Participation?> GetByIdsAsync(string userId, int expenseId)
    {
        return await _dbContext.Participations
            .Include(p => p.User)
            .Include(p => p.Expense)
            .FirstOrDefaultAsync(p => p.UserId == userId && p.ExpenseId == expenseId);
    }

    public async Task<Participation?> GetByIdsIncludingAsync(
      string userId,
      int expenseId,
      bool includeUser = false,
      bool includeExpense = false)
    {
        var query = _dbContext.Participations.AsQueryable();

        if (includeUser)
        {
            query = query.Include(ug => ug.User);
        }

        if (includeExpense)
        {
            query = query.Include(ug => ug.Expense);
        }

        return await query.SingleOrDefaultAsync(ug => ug.UserId == userId && ug.ExpenseId == expenseId);
    }
}
