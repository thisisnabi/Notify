namespace Notify.Features.Sms;

public interface ISmsProvider
{
    Task<string> SendAsync(string mobile, string message, CancellationToken cancellationToken = default);

    Task<SmsTraceStatus> IquiryAsync(string referenceId, CancellationToken cancellationToken = default);

    string Name { get; }
}
