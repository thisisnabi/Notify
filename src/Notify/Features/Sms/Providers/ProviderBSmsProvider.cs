namespace Notify.Features.Sms.Providers;

public class ProviderBSmsProvider(IOptions<AppSettings> appSettingOptions) : ISmsProvider
{
    private readonly ProviderBConfiguration configuration = appSettingOptions.Value.Features.Sms.ProviderB;

    public string Name => "ProviderB";


    public Task<string> SendAsync(string mobile, string message, CancellationToken cancellationToken = default)
    {
        // send your sms request by REST API
        // using {_httpClient}

        return Task.FromResult(string.Empty);
    }


    public Task<SmsTraceStatus> IquiryAsync(string referenceId, CancellationToken cancellationToken = default)
    {
        // Get info from the provider by REST API
        // using {_httpClient}
        return Task.FromResult(SmsTraceStatus.RequiredInquiry);
    }
}
