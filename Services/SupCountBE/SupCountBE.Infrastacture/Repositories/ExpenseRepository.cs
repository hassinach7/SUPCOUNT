using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class ExpenseRepository : AsyncRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Expense?> GetByIdIncludingAsync(
        int id,
        bool includePayer = false,
        bool includeCategory = false,
        bool includeGroup = false,
        bool includeParticipations = false,
        bool includeJustifications = false)
    {
        var query = _dbContext.Expenses.AsQueryable();

        if (includePayer)
        {
            query = query.Include(e => e.Payer);
        }

        if (includeCategory)
        {
            query = query.Include(e => e.Category);
        }

        if (includeGroup)
        {
            query = query.Include(e => e.Group);
        }

        if (includeParticipations)
        {
            query = query.Include(e => e.Participations);
        }

        if (includeJustifications)
        {
            query = query.Include(e => e.Justifications);
        }

        return await query.SingleOrDefaultAsync(e => e.Id == id);
    }
}
