namespace Notify.Features.Sms.Services;

public class SmsService(SmsDbContext dbContext, IServiceProvider serviceProvider , IEnumerable<ISmsProvider> smsProviders)
{
    private readonly SmsDbContext _dbContext = dbContext;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly IEnumerable<ISmsProvider> _smsProviders = smsProviders;

    public async Task SendAsync(Guid messageId, string mobile, string message, CancellationToken cancellationToken)
    {
        foreach (var smsProvider in _smsProviders)
        {
            var referenceId = await smsProvider.SendAsync(mobile, message, cancellationToken);

            if (string.IsNullOrEmpty(referenceId))
            {
                // try by sending with another provider
                continue;
            }

            var smsTrace = SmsTrace.Create(mobile, message, messageId, referenceId, smsProvider.Name);
            await _dbContext.SmsTraces.AddAsync(smsTrace, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // end of try
            break;
        }
       
    }

    public async Task<SmsTraceStatus> InquiryAsync(SmsTrace message, CancellationToken cancellationToken = default)
    {
        var provider = _smsProviders.FirstOrDefault(a => a.Name == message.Provider);
        if (provider == null)
            throw new Exception($"sms provider {message.Provider} not found use correct sms provider");
        return await provider.IquiryAsync(message.RefrenceId, cancellationToken);
    }
}
