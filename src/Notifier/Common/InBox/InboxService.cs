using Microsoft.EntityFrameworkCore;
using Notifier.Common.Persistence;

namespace Notifier.Common.InBox
{
    public class InboxService(NotifierDbContext dbContext)
    {
        private readonly NotifierDbContext _dbContext = dbContext;

        public async Task CreateAsync(InboxMessage inboxMessage, CancellationToken cancellationToken = default)
        {
            await _dbContext.InboxMessages.AddAsync(inboxMessage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> FindMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : BaseMessage
        {
            return await _dbContext.InboxMessages.AnyAsync(x => x.MessageId == message.MessageId, cancellationToken);
        }

        public Task<List<InboxMessage>> GetUnProcessedMessagesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.InboxMessages
                             .Where(x => !x.Processed)
                             .OrderBy(x => x.CreatedOn)
                             .ToListAsync(cancellationToken);
        }

        internal async Task ProcessMessagesAsync(InboxMessage message, CancellationToken cancellationToken)
        {
            message.Process();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
