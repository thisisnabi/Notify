namespace Notify.Features.Email.REST.SendEmail;

public record SendEmailRequest(Guid MessageId, string To, string Subject, string Body);