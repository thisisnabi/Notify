namespace Notify.Features.Sms;

public class SmsService(SmsDbContext dbContext, IServiceProvider serviceProvider)
{
    private readonly SmsDbContext _dbContext = dbContext;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task SendAsync(Guid messageId,string mobile, string message, CancellationToken cancellationToken)
    {

        foreach (var providerName in SmsConfiguration.Providers)
        {
            var provider = _serviceProvider.GetRequiredKeyedService<ISmsProvider>(providerName);

            var referenceId = await provider.SendAsync(mobile, message, cancellationToken);

            if (string.IsNullOrEmpty(referenceId))
            {
                // try by sending with another provider
                continue;
            }

            var smsTrace = SmsTrace.Create(mobile, message, messageId, referenceId, provider.Name);
            await _dbContext.SmsTraces.AddAsync(smsTrace, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // end of try
            break;
        }
    }

    public async Task<SmsTraceStatus> InquiryAsync(SmsTrace message, CancellationToken cancellationToken = default)
    {
        var provider = _serviceProvider.GetRequiredKeyedService<ISmsProvider>(message.Provider);
        return await provider.IquiryAsync(message.RefrenceId, cancellationToken);
    }
}
