namespace Notify;
public partial class FeatureConfiguration
{
    public SmsConfiguration Sms { get; set; } = null!;
}

public class SmsConfiguration
{
    public ProviderAConfiguration ProviderA { get; set; } = null!;
    public ProviderBConfiguration ProviderB { get; set; } = null!;

    public readonly static IList<string> Providers = ["ProviderA", "ProviderB"];

}

public class ProviderAConfiguration
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Number { get; set; }
}

public class ProviderBConfiguration
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Number { get; set; }
}
