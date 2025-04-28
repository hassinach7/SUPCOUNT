namespace SupCountBE.Core.Repositories;

public interface IUserRepository : IAsyncRepository<User>
{
    Task<User?> GetByIdIncludingAsync(
        string id,
        bool includeExpenses = false,
        bool includeParticipations = false,
        bool includeReimbursementsSent = false,
        bool includeReimbursementsReceived = false,
        bool includeSentMessages = false,
        bool includeReceivedMessages = false,
        bool includeUserGroups = false
    );
    Task<IList<User>> GetAllListIncludingAsync(
           bool includeExpenses = false,
           bool includeReimbursements = false,
           bool includeGroups = false
       );
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetReciepientByIdAsync(string RecipientId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<(bool,string)> CreateAsync(User user, string password);
}
