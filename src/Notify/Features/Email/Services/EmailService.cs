using Notify.Features.Email.Dtos;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Notify.Features.Email.Services;

public class EmailService(
    EmailDbContext dbContext,
    IOptions<AppSettings> appsettingsOption)
{
    private readonly AppSettings _appsettings = appsettingsOption.Value;
    private readonly EmailConfiguration _emailConfiguration = appsettingsOption.Value.Features.Email;
    private readonly EmailDbContext _dbContext = dbContext;

    public async Task SendEmailAsync(EmailMessageDto emailMessageDto, CancellationToken cancellationToken)
    {
        var trackId = OnSendEmail(emailMessageDto);

        var emailTrace = CreateEmailTrace(emailMessageDto, trackId);

        await OnSaveEmailTraces(emailTrace, cancellationToken);
    }

    private string OnSendEmail(EmailMessageDto emailMessageDto)
    {
        var smtpClient = CreateSmtpClient();

        var trackId = GenerateUniqueTrackId();

        var mailMessage = CreateMailMessage(emailMessageDto, trackId);

        smtpClient.Send(mailMessage);

        return trackId;
    }

    private async Task OnSaveEmailTraces(EmailTrace emailTrace, CancellationToken cancellationToken)
    {
        await _dbContext.EmailTraces.AddAsync(emailTrace, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private SmtpClient CreateSmtpClient()
        => new(_emailConfiguration.Host)
        {
            Port = _emailConfiguration.Port,
            Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
            EnableSsl = true
        };

    private MailMessage CreateMailMessage(EmailMessageDto emailMessageDto, string trackId)
    {
        StringBuilder htmlBody = new StringBuilder();

        htmlBody.Append(emailMessageDto.Body);
        htmlBody.Append(GenerateTrackingPixel(trackId));

        var mailMessage = new MailMessage(
            _emailConfiguration.SenderEmail,
            emailMessageDto.To,
            emailMessageDto.Subject,
            htmlBody.ToString());

        mailMessage.IsBodyHtml = true;

        return mailMessage;
    }

    private EmailTrace CreateEmailTrace(EmailMessageDto emailMessageDto, string trackId)
        => EmailTrace.Create(emailMessageDto.To,
            emailMessageDto.Subject,
            emailMessageDto.Body,
            emailMessageDto.MessageId,
            trackId);

    private string GenerateTrackingPixel(string trackId)
    {
        return $"<img width='1' height='1' src='{_appsettings.BaseUrl}/email/tracking/{trackId}'>";
    }

    private string GenerateUniqueTrackId()
    {
        return Guid.NewGuid().ToString();
    }

}
