namespace Notify.Features.Sms.REST.SendSms;

public record SendSmsRequest(Guid MessageId, string Mobile, string Message);