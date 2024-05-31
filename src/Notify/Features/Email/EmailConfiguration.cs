namespace Notify;
public partial class FeatureConfiguration
{
    public EmailConfiguration Email { get; set; } = null!;
}

public class EmailConfiguration
{
    public required string UserName { get; set; }
    public required string Password { get; set; }

    public required string Host { get; set; }
    public required string SenderEmail { get; set; }

    public int Port { get; set; }
}

