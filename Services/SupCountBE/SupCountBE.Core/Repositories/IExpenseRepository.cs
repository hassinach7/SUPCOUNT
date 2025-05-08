namespace SupCountBE.Core.Repositories;

public interface IExpenseRepository : IAsyncRepository<Expense>
{
    Task<Expense?> GetByIdIncludingAsync(int id, IncludingProperties includingProperties);

    Task<IList<Expense>> GetAllListIncludingAsync(IncludingProperties includingProperties);
    Task<IList<Expense>> GetAllExpenseByGroupAsync(int groupId, string userId);
}

public record IncludingProperties
{
    public bool IncludePayer { get; set; } = false;
    public bool IncludeCategory { get; set; } = false;
    public bool IncludeGroup { get; set; } = false;
    public bool IncludeParticipations { get; set; } = false;
    public bool IncludeJustifications { get; set; } = false;
}
