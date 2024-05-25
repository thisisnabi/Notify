using MediatR;
using Notifier.Features.Sms.Consumers;

namespace Notifier.Features.Sms.Handlers
{
    public class SendSmsHandler(SmsService smsService) : INotificationHandler<SmsNotify>
    {
        private readonly SmsService _smsService = smsService;

        public async Task Handle(SmsNotify notification, CancellationToken cancellationToken)
        {
            await _smsService.SendAsync(notification.Mobile, notification.Message, cancellationToken);
        }
    }
}
