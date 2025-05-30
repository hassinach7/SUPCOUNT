﻿using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;

namespace SupCountBE.Infrastructure.Repositories;

public class MessageRepository : AsyncRepository<Message>, IMessageRepository
{
    public MessageRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Message?> GetByIdIncludingAsync(int id, MessageIncludingProperties messageIncludingProperties)
    {
        var query = Get(messageIncludingProperties);
        return await query.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IList<Message>> GetAllListIncludingAsync(MessageIncludingProperties messageIncludingProperties)
    {
        var query = Get(messageIncludingProperties);
        return await query.ToListAsync();
    }

    private IQueryable<Message> Get(MessageIncludingProperties props)
    {
        var query = _dbContext.Messages.AsQueryable();

        if (props.IncludeSenders)
        {
            query = query.Include(m => m.Sender);
        }

        if (props.IncludeRecipients)
        {
            query = query.Include(m => m.Recipient);
        }

        if (props.IncludeGroups)
        {
            query = query.Include(m => m.Group);
        }

        return query;
    }

    public async Task<IList<Message>> GetPrivateMessagesAsync(string userId)
    {
       return await _dbContext.Messages
            .Include(o=> o.Sender)
            .Include(o => o.Recipient)
            .Where(m => m.IsPrivate && (m.SenderId == userId || m.RecipientId == userId))
            .ToListAsync();
    }
}
