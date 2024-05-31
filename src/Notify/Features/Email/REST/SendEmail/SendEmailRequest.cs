namespace Notify.Features.Email.REST.SendSms;

public record SendEmailRequest(Guid MessageId, string To, string Subject, string Body);