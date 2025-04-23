
namespace SupCountBE.Core.Repositories;

public interface ICategoryRepository : IAsyncRepository<Category>
{
    Task<Category?> GetByIdIncludingAsync
        (int id,
        bool includeExpenses = false
        );
   
}
