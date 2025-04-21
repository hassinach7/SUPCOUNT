namespace SupCountBE.Core.Repositories;

public interface IMessageRepository : IAsyncRepository<Message>
{
    Task<Message?> GetByIdIncludingAsync(
        int id,
        bool includeSender = false,
        bool includeRecipient = false,
        bool includeGroup = false
    );
    Task<IList<Message>> GetAllListIncludingAsync(
       bool includeSender = false,
       bool includeRecipient = false,
       bool includeGroup = false);
}
