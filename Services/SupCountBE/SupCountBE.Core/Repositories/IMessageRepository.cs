namespace SupCountBE.Core.Repositories;

public interface IMessageRepository : IAsyncRepository<Message>
{
    Task<Message?> GetByIdIncludingAsync(int id, MessageIncludingProperties messageIncludingProperties );
    Task<IList<Message>> GetAllListIncludingAsync(MessageIncludingProperties messageIncludingProperties );
}
public record MessageIncludingProperties
{
    public bool IncludeSender { get; set; } = false;
    public bool IncludeRecipient { get; set; } = false;
    public bool IncludeGroup { get; set; } = false;
}
