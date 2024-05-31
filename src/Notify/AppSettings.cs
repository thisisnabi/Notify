namespace Notify;

public class AppSettings
{
    public SvcDbContextConfiguraion SvcDbContext { get; set; } = null!;

}
 
public class SvcDbContextConfiguraion
{
    public required string Host { get; set; }
    public required string DatabaseName { get; set; }
}