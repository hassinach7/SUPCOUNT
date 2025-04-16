using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class CategoryRepository : AsyncRepository<Category>, ICategoryRepository
{
    public CategoryRepository(SupCountDbContext dbContext) : base(dbContext)
    {
        
    }
    public async Task<Category?> GetByIdIncludingAsync(int id , bool includeExpenses = false)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (includeExpenses)
        {
            query = query.Include(c => c.Expenses);
        }

        return await query.SingleOrDefaultAsync(o => o.Id == id);
    }
}
