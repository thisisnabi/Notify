namespace Notify.Features.Email.Messages;

public record SendEmailMessage(Guid MessageId, string To, string Subject, string Body)
    : IntegrationMessage(MessageId);

