namespace SupCountBE.Core.Repositories;

public interface IMessageRepository : IAsyncRepository<Message>
{
    Task<Message?> GetByIdIncludingAsync(int id, MessageIncludingProperties messageIncludingProperties );
    Task<IList<Message>> GetAllListIncludingAsync(MessageIncludingProperties messageIncludingProperties );
    Task<IList<Message>> GetPrivateMessagesAsync(string userId);
}
public record MessageIncludingProperties
{
    public bool IncludeSenders { get; set; } = false;
    public bool IncludeRecipients { get; set; } = false;
    public bool IncludeGroups { get; set; } = false;
}
