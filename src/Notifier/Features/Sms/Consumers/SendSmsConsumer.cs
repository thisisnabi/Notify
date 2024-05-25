using MassTransit;
using Notifier.Common;
using Notifier.Common.InBox;

namespace Notifier.Features.Sms.Consumers;

public class SmsNotifyConsumer(InboxService inboxService) : IConsumer<SmsNotify>
{
    private readonly InboxService _inboxService = inboxService;

    public async Task Consume(ConsumeContext<SmsNotify> context)
    {
        if (await _inboxService.FindMessageAsync(context.Message, context.CancellationToken))
            return;

        var inboxMessage = InboxMessage.Create(context.Message);
        await _inboxService.CreateAsync(inboxMessage, context.CancellationToken);
    }
}

public record SmsNotify(Guid MessageId,string Mobile, string Message) : BaseMessage(MessageId);

