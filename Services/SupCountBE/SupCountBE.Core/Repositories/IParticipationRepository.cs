namespace SupCountBE.Core.Repositories;

public interface IParticipationRepository : IAsyncRepository<Participation>
{
    Task<IList<Participation>> GetListIncludingAsync(
       
        bool includeUser = false,
        bool includeExpense = false
    );
}
