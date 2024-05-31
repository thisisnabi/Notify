namespace Notify.Features.Sms;

public class SmsDbContext : DbContext
{
    public SmsDbContext(DbContextOptions<SmsDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<SmsTrace> SmsTraces => Set<SmsTrace>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SmsTrace>().ToCollection("sms_traces");
    }
}
