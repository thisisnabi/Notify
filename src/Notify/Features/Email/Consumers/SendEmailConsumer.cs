namespace Notify.Features.Sms.Consumers;

public class SendEmailConsumer(InboxService inboxService)
    : InboxConsumer<SendEmailMessage>(inboxService)
{

}