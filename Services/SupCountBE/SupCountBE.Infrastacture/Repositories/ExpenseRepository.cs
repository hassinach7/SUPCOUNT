using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SupCountBE.Infrastacture.Repositories;

public class ExpenseRepository : AsyncRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public Task<IList<Expense>> GetAllListIncludingAsync(IncludingProperties includingProperties)
    {
        var query = Get(includingProperties);
        return Task.FromResult(query.ToList() as IList<Expense>);
    }

    private IQueryable<Expense> Get(IncludingProperties includingProperties)
    {
        var query = _dbContext.Expenses.AsQueryable();

        if (includingProperties.IncludePayer)
        {
            query = query.Include(e => e.Payer);
        }

        if (includingProperties.IncludeCategory)
        {
            query = query.Include(e => e.Category);
        }

        if (includingProperties.IncludeGroup)
        {
            query = query.Include(e => e.Group);
        }

        if (includingProperties.IncludeParticipations)
        {
            query = query.Include(e => e.Participations);
        }

        if (includingProperties.IncludeJustifications)
        {
            query = query.Include(e => e.Justifications);
        }

        return query;
    }

    public async Task<Expense?> GetByIdIncludingAsync(int id, IncludingProperties includingProperties)
    {
        var query = Get(includingProperties);

        return await query.SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IList<Expense>> GetAllExpenseByGroupAsync(int groupId, string userId)
    {
        return await _dbContext.Expenses
              .Include(e => e.Payer)
              .Include(e => e.Category)
              .Include(e => e.Group)
              .ThenInclude(e => e!.UserGroups)
              .Include(e => e.Participations)
              .Include(e => e.Justifications)
              .Where(e => e.GroupId == groupId && e.Group!.UserGroups!.Any(ug => ug.UserId == userId)).ToListAsync();
    }
}
