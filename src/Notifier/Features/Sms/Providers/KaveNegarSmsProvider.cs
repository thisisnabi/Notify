using PayamakCore.Dto;
using PayamakCore.Interface;

namespace Notifier.Features.Sms.Providers;

public class KaveNegarSmsProvider(IPayamakServices payamakServices,
                                    IOptions<AppSettings> appSettingOptions) : ISmsProvider
{
    private readonly IPayamakServices _payamakServices = payamakServices;
    private readonly KavehNegarConfiguration configuration = appSettingOptions.Value.Features.Sms.KavehNegar;

    public string Name => "KavehNegar";

  
    public Task<string?> SendAsync(string mobile, string message, CancellationToken cancellationToken = default)
    {
    
        throw new NotImplementedException();
    }


    public Task<SmsTraceStatus> IquiryAsync(string referenceId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
