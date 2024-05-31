namespace Notify.Features.Sms.Handlers;

public class SendSmsHandler(SmsService smsService) : INotificationHandler<SendSmsMessage>
{
    private readonly SmsService _smsService = smsService;

    public async Task Handle(SendSmsMessage notification, CancellationToken cancellationToken)
    {
        await _smsService.SendAsync(notification.MessageId ,
            notification.Mobile, 
            notification.Message, cancellationToken);
    }
}