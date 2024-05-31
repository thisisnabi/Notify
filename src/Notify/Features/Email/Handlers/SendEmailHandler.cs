namespace Notify.Features.Email.Handlers;

public class SendEmailHandler(EmailService emailService) : INotificationHandler<SendEmailMessage>
{
    private readonly EmailService _emailService = emailService;

    public Task Handle(SendEmailMessage notification, CancellationToken cancellationToken)
    {
        return _emailService.SendEmailAsync(notification.To, 
            notification.Subject, 
            notification.Body, 
            notification.MessageId, 
            cancellationToken);
    }
}