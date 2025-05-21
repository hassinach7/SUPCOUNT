namespace SupCountBE.Core.Repositories;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<Group?> GetByIdIncludingAsync(int id, GroupIncludingProperties groupIncludingProperties );
       
    
    Task<IList<Group>> GetAllListIncludingAsync(
        bool includeUserGroups = false,
        bool includeExpenses = false,
        bool includeReimbursements = false,
        bool includeMessages = false
    );
}
public record class GroupIncludingProperties
{
    public bool IncludeUserGroups { get; set; } = false;
    public bool IncludeExpenses { get; set; } = false;
    public bool IncludeReimbursements { get; set; } = false;
    public bool IncludeMessages { get; set; } = false;
}