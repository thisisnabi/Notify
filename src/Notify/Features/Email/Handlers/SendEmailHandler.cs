﻿namespace Notify.Features.Email.Handlers;

public class SendEmailHandler(EmailService emailService) : INotificationHandler<SendEmailMessage>
{
    private readonly EmailService _emailService = emailService;

    public Task Handle(SendEmailMessage notification, CancellationToken cancellationToken)
    {
        var emailMessageDto = CreateEmailMessageDto(notification);

        return _emailService.SendEmailAsync(emailMessageDto, cancellationToken);
    }

    private EmailMessageDto CreateEmailMessageDto(SendEmailMessage notification)
        => new EmailMessageDto(
            notification.MessageId,
            notification.To,
            notification.Subject,
            notification.Body);

}