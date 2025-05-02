using Microsoft.EntityFrameworkCore;
using SupCountBE.Core.Entities;
using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class JustificationRepository : AsyncRepository<Justification>, IJustificationRepository
{
    public JustificationRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<IList<Justification>> GetAllListByExpenseIdIncludingAsync(int expenseId, bool includeExpense = false)
    {
        var query = _dbContext.Justifications.AsQueryable();

        if (includeExpense)
        {
            query = query.Include(j => j.Expense);
        }

        return await query
            .Where(j => j.ExpenseId == expenseId)
            .ToListAsync();
    }

    public async Task<Justification?> GetByIdIncludingAsync(int id, bool includeExpense = false)
    {
        var query = _dbContext.Justifications.AsQueryable();

        if (includeExpense)
        {
            query = query.Include(j => j.Expense);
        }

        return await query.SingleOrDefaultAsync(j => j.Id == id);
    }

}
