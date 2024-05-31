namespace Notify.Common.Inbox;

public class InboxConsumer<TMessage>(InboxService inboxService)
    : IConsumer<TMessage> where TMessage : IntegrationMessage
{
    private readonly InboxService _inboxService = inboxService;

    public virtual async Task Consume(ConsumeContext<TMessage> context)
    {
        if (await _inboxService.HasMessageAsync(context.Message, context.CancellationToken))
            return;

        var inboxMessage = InboxMessage.Create(context.Message);
        await _inboxService.AddAsync(inboxMessage, context.CancellationToken);
    }
}