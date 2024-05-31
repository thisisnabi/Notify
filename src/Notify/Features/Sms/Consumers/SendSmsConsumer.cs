namespace Notify.Features.Sms.Consumers;

public class SendSmsConsumer(InboxService inboxService)
    : InboxConsumer<SendSmsMessage>(inboxService)
{

}