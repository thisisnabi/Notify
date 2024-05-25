using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Notifier.Common.InBox;
using Notifier.Features.Sms;

namespace Notifier.Common.Persistence;

public class NotifierDbContext : DbContext
{
    public NotifierDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
    {
        
    }

    public DbSet<SmsTrace> SmsTraces => Set<SmsTrace>(); 
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SmsTrace>().ToCollection("sms_traces");
        modelBuilder.Entity<InboxMessage>().ToCollection("inbox_messages");
    }
}
