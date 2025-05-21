namespace SupCountBE.Core.Repositories;

public interface IParticipationRepository : IAsyncRepository<Participation>
{
    Task<IList<Participation>> GetListIncludingAsync(
       ParticipationIncludingProperties participationIncludingProperties
    );

    Task<Participation?> GetByIdsAsync( int expenseId);
    Task<Participation?> GetByIdsIncludingAsync(
      
        int expenseId,
        ParticipationIncludingProperties participationIncludingProperties
        );
}

public record ParticipationIncludingProperties
{
    public bool IncludeUser { get; set; } = false;
    public bool IncludeExpense { get; set; } = false;
}