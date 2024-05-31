namespace Notify.Features.Sms.Consumers;

public class SmsNotifyConsumer(InboxService inboxService)
    : InboxConsumer<SendSmsMessage>(inboxService)
{

}