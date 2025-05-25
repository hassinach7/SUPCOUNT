namespace SupCountBE.Core.Repositories;

public interface IParticipationRepository : IAsyncRepository<Participation>
{
    Task<IList<Participation>> GetListIncludingAsync(
       ParticipationIncludingProperties participationIncludingProperties
    );

    Task<Participation?> GetByIdsAsync(int expenseId);
    Task<Participation?> GetByIdsIncludingAsync(

        int expenseId,
        ParticipationIncludingProperties participationIncludingProperties
        );
    Task<IList<Participation>> GetListByUserIdAsync(string userId);
}

public record ParticipationIncludingProperties
{
    public bool IncludeUsers { get; set; } = false;
    public bool IncludeExpenses { get; set; } = false;
}