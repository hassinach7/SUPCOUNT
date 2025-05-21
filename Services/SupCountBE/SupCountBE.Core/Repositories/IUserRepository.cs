namespace SupCountBE.Core.Repositories;

public interface IUserRepository : IAsyncRepository<User>
{
    Task UpdateAsync(User user , List<string> roles);

    Task<IList<string>> GetRolesByUserIdAsync(string userId);
    Task<User?> GetByIdIncludingAsync(string id, IncludingItem including);
    Task<IList<User>> GetAllListIncludingAsync(IncludingItem including);
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetReciepientByIdAsync(string RecipientId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<(bool, string)> CreateAsync(User user, string password,IList<string> roles);
    Task<IList<User>> GetAllUsersByGroupIdAsync(int groupId);
}
public class IncludingItem
{
    public string Id { get; set; } = null!;
    public bool IncludeExpenses { get; set; } = false;
    public bool IncludeParticipations { get; set; } = false;
    public bool IncludeReimbursementsSent { get; set; } = false;
    public bool IncludeReimbursementsReceived { get; set; } = false;
    public bool IncludeSentMessages { get; set; } = false;
    public bool IncludeReceivedMessages { get; set; } = false;
    public bool IncludeUserGroups { get; set; } = false;
    public bool IncludeGroups { get; set; } = false;
    public bool IncludeReimbursements { get; set; } = false;
}
