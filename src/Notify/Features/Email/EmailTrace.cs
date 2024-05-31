namespace Notify.Features.Email;

[Collection("email_traces")]
public class EmailTrace
{
    public ObjectId Id { get; set; }

    public required string To { get; set; }

    public required string Subject { get; set; }

    public required string Body { get; set; }

    public required EmailTraceStatus Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid MessageId { get; set; }

    public required string TrackerId { get; set; }

    public static EmailTrace Create(
        string to,
        string subject,
        string body,
        Guid messageId,
        string trackId) => new EmailTrace
        {
            To = to,
            Body = body,
            Status = EmailTraceStatus.Sent,
            CreatedOn = DateTime.UtcNow,
            MessageId = messageId,
            Subject = subject,
            TrackerId = trackId
        };
}