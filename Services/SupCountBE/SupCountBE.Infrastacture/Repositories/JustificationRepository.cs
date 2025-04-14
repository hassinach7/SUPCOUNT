using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class JustificationRepository : AsyncRepository<Justification>, IJustificationRepository
{
    public JustificationRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Justification?> GetByIdIncludingAsync(int id, bool includeExpenses = false)
    {
        var query = _dbContext.Justifications.AsQueryable();

        if (includeExpenses)
        {
            query = query.Include(j => j.Expense);
        }

        return await query.SingleOrDefaultAsync(j => j.Id == id);
    }
}
