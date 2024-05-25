using PayamakCore.Dto;
using PayamakCore.Interface;

namespace Notifier.Features.Sms.Providers;

public class FarapayamakSmsProvider(IPayamakServices payamakServices,
                                    IOptions<AppSettings> appSettingOptions) : ISmsProvider
{
    private readonly IPayamakServices _payamakServices = payamakServices;
    private readonly FarapayamakConfiguration configuration = appSettingOptions.Value.Features.Sms.Farapayamak;

    public string Name => "Farapayamak";

    private readonly static IList<string> InvalidStatus = ["0","1","2","3","4","5","6","7","9","10","11","12","14","15"];
     
    public async Task<string?> SendAsync(string mobile, string message, CancellationToken cancellationToken = default)
    {
    
        var result = await _payamakServices.SendSms(new MessageDto
        {
            From = configuration.Number,
            To = mobile,
            Text = message,
            username = configuration.UserName,
            password = configuration.Password
        }, cancellationToken);

        if (InvalidStatus.Any(x => x == result.Value))
            return null;

        return result.Value;
    }


    public async Task<SmsTraceStatus> IquiryAsync(string referenceId, CancellationToken cancellationToken = default)
    {
        var recId = Convert.ToInt64(referenceId);
         
        var result = await _payamakServices.GetMessageStatus(new DeliverRequestDto
        {
            password = configuration.Password,
            RecId = recId,
            username = configuration.UserName
        }, cancellationToken);
         
        if(result.RetStatus == 1)
            return SmsTraceStatus.Deliveried;

        return SmsTraceStatus.Failed;
    }
}
