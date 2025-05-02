namespace SupCountBE.Core.Repositories;

public interface IJustificationRepository : IAsyncRepository<Justification>
{
    Task<Justification?> GetByIdIncludingAsync(
        int id,
        bool includeExpense = false
    );
 
    Task<IList<Justification>> GetAllListByExpenseIdIncludingAsync(
        int expenseId,
        bool includeExpense = false
    );
}
