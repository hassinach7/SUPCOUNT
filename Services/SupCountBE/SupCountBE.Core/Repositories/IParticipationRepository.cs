namespace SupCountBE.Core.Repositories;

public interface IParticipationRepository : IAsyncRepository<Participation>
{
    Task<IList<Participation>> GetListIncludingAsync(
        bool includeUser = false,
        bool includeExpense = false
    );

    Task<Participation?> GetByIdsAsync(string userId, int expenseId);
    Task<Participation?> GetByIdsIncludingAsync(
        string userId, 
        int expenseId,
         bool includeUser = false,
        bool includeExpense = false
        );



}
