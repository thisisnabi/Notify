using System.Net;
using System.Net.Mail;

namespace Notify.Features.Email;

public class EmailService(
    EmailDbContext dbContext,
    IOptions<AppSettings> appsettingsOption)
{
    private readonly AppSettings _appsettings = appsettingsOption.Value;
    private readonly EmailConfiguration _emailConfiguration = appsettingsOption.Value.Features.Email;
    private readonly EmailDbContext _dbContext = dbContext;

    public async Task SendEmailAsync(string to, string subject, string body, Guid messageId, CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient(_emailConfiguration.Host)
        {
            Port = _emailConfiguration.Port,
            Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
            EnableSsl = true
        };

        var trackId = GenerateUniqueTrackId();
        var htmlBody = body + GenerateTrackingPixel(trackId);

        var mailMessage = new MailMessage(_emailConfiguration.SenderEmail, to, subject, htmlBody);
        mailMessage.IsBodyHtml = true;

        smtpClient.Send(mailMessage);

        var emailTrace = EmailTrace.Create(to, subject, body, messageId, trackId);
        await _dbContext.EmailTraces.AddAsync(emailTrace, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public string GenerateTrackingPixel(string trackId)
    {
        return $"<img width='1' height='1' src='{_appsettings.BaseUrl}/email/tracking/{trackId}'>";
    }

    public string GenerateUniqueTrackId()
    {
        return Guid.NewGuid().ToString();
    }
}
