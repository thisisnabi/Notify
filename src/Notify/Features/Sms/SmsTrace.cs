namespace Notify.Features.Sms;

[Collection("sms_traces")]
public class SmsTrace
{
    public ObjectId Id { get; set; }

    public required string Mobile { get; set; }

    public required string Message { get; set; }

    public required string RefrenceId { get; set; }

    public required SmsTraceStatus Status { get; set; }

    public string Provider { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public Guid MessageId { get; set; }

    public static SmsTrace Create(
        string mobile, 
        string message,
        Guid messageId,
        string referenceId, 
        string provider) => new SmsTrace
    {
        Mobile = mobile,
        Message = message,
        RefrenceId = referenceId,
        Status = SmsTraceStatus.RequiredInquiry,
        CreatedOn = DateTime.UtcNow,
        Provider = provider,
        MessageId = messageId,
    };
}