namespace SupCountBE.Core.Repositories;

public interface IJustificationRepository : IAsyncRepository<Justification>
{
    Task<Justification?> GetByIdIncludingAsync(
        int id,
        bool includeExpenses = false
    );
    Task<IList<Justification>> GetAllListIncludingAsync(
        bool includeExpense = false
    );
}
