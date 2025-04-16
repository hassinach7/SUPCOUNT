using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class MessageRepository : AsyncRepository<Message>, IMessageRepository
{
    public MessageRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Message?> GetByIdIncludingAsync(
        int id,
        bool includeSender = false,
        bool includeRecipient = false,
        bool includeGroup = false)
    {
        var query = _dbContext.Messages.AsQueryable();

        if (includeSender)
        {
            query = query.Include(m => m.Sender);
        }

        if (includeRecipient)
        {
            query = query.Include(m => m.Recipient);
        }

        if (includeGroup)
        {
            query = query.Include(m => m.Group);
        }

        return await query.SingleOrDefaultAsync(m => m.Id == id);
    }
}
