namespace SupCountBE.Core.Repositories;

public interface IJustificationRepository : IAsyncRepository<Justification>
{
    Task<Justification?> GetByIdIncludingAsync(
        int id,
        bool includeExpenses = false
    );
}
