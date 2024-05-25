namespace Notifier;

public class AppSettings
{

    public FeatureConfiguration Features { get; set; } = null!;
    public SvcDbContextConfiguraion SvcDbContext { get; set; } = null!;
    public BrokerConfiguration BrokerConfiguration { get; set; } = null!;
}

public partial class FeatureConfiguration
{

}

public class SvcDbContextConfiguraion
{
    public required string Host { get; set; }
    public required string DatabaseName { get; set; }
}
public class BrokerConfiguration
{
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}