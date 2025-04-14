namespace SupCountBE.Core.Repositories;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<Group?> GetByIdIncludingAsync(
        int id,
        bool includeUserGroups = false,
        bool includeExpenses = false,
        bool includeReimbursements = false,
        bool includeMessages = false
    );
}
