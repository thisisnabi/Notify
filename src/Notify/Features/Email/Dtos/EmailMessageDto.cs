namespace Notify.Features.Email.Dtos;

public record EmailMessageDto(Guid MessageId, string To, string Subject, string Body)
{
}
