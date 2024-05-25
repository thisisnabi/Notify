namespace Notifier;

public partial class FeatureConfiguration
{
    public SmsConfiguration Sms { get; set; } = null!;

}

public class SmsConfiguration
{
    public FarapayamakConfiguration Farapayamak { get; set; } = null!;
    public KavehNegarConfiguration KavehNegar { get; set; } = null!;

}
 
public class FarapayamakConfiguration
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Number { get; set; }
}



public class KavehNegarConfiguration
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Number { get; set; }
}
