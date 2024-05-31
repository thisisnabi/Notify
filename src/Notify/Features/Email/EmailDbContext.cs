namespace Notify.Features.Email;

public class EmailDbContext : DbContext
{
    public EmailDbContext(DbContextOptions<EmailDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<EmailTrace> EmailTraces => Set<EmailTrace>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmailTrace>().ToCollection("email_traces");
    }
}
