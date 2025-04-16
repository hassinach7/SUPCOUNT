namespace SupCountBE.Core.Repositories;

public interface IParticipationRepository : IAsyncRepository<Participation>
{
    Task<Participation?> GetByIdIncludingAsync(
        int id,
        bool includeUser = false,
        bool includeExpense = false
    );
}
