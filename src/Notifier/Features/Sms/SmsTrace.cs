using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Notifier.Features.Sms;

[Collection("sms_traces")]
public class SmsTrace
{

    public ObjectId Id { get; set; }

    public required string Mobile { get; set; }
    public required string Message { get; set; }
    public required string RefrenceId { get; set; }
    public required SmsTraceStatus Status { get; set; }
    public string? Provider { get; set; }
    public DateTime CreatedOn { get; set; }

    public static SmsTrace Create(string mobile, string message, string referenceId, string provider) => new SmsTrace
    {
        Mobile = mobile,
        Message = message,
        RefrenceId = referenceId,
        Status = SmsTraceStatus.Inquiry,
        CreatedOn = DateTime.Now,
        Provider = provider
    };

}
 
public enum SmsTraceStatus
{
    Inquiry = 0,
    Failed = 1,
    Deliveried = 2 ,
    None = 100
}