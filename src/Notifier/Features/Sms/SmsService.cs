using Notifier.Common.Persistence;

namespace Notifier.Features.Sms;

public class SmsService(
    NotifierDbContext dbContext,
    IServiceProvider serviceProvider)
{
    private readonly NotifierDbContext _dbContext = dbContext;

    public async Task SendAsync(string mobile, string message, CancellationToken cancellationToken)
    {
        var services = serviceProvider.GetKeyedServices<ISmsProvider>("Farapayamak");

        foreach (var smsProvider in services)
        {
            var referenceId = await smsProvider.SendAsync(mobile, message, cancellationToken);

            if (string.IsNullOrEmpty(referenceId))
            {
                throw new InvalidOperationException();
            }

            var smsTrace = SmsTrace.Create(mobile, message, referenceId, smsProvider.Name);

            await _dbContext.SmsTraces.AddAsync(smsTrace, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            break;
        }
    }

    internal async Task<SmsTraceStatus> InquiryAsync(SmsTrace message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(message.Provider))
            return SmsTraceStatus.None;

        var provider = serviceProvider.GetRequiredKeyedService<ISmsProvider>(message.Provider);
        return await provider.IquiryAsync(message.RefrenceId, cancellationToken);
    }
}
