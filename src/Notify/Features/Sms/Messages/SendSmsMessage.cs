namespace Notify.Features.Sms.Messages;
 
public record SendSmsMessage(Guid MessageId, string Mobile, string Message) 
    : IntegrationMessage(MessageId);

