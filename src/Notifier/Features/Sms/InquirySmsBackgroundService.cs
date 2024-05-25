
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notifier.Common.Persistence;

namespace Notifier.Features.Sms
{
    public class InquirySmsBackgroundService(
      IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scoped = serviceProvider.CreateScope();
            var _dbContext = scoped.ServiceProvider.GetRequiredService<NotifierDbContext>();
            var _smsService = scoped.ServiceProvider.GetRequiredService<SmsService>();
             
            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = await _dbContext.SmsTraces.Where(x => x.Status == SmsTraceStatus.Inquiry)
                                                         .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    message.Status = await _smsService.InquiryAsync(message, stoppingToken);
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(5000);

            }

        }
    }
}
