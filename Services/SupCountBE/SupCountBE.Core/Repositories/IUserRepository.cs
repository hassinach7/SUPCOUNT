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
}
