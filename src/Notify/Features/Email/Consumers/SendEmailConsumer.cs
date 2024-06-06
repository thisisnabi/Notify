namespace Notify.Features.Email.Consumers;

public class SendEmailConsumer(InboxService inboxService)
    : InboxConsumer<SendEmailMessage>(inboxService)
{

}