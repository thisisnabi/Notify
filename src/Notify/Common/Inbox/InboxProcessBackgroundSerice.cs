namespace Notify.Common.Inbox;

public class InboxProcessBackgroundSerice(IServiceProvider serviceProvider,
    ILogger<InboxProcessBackgroundSerice> logger) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<InboxProcessBackgroundSerice> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scoped = _serviceProvider.CreateScope();
        var _inboxService = scoped.ServiceProvider.GetRequiredService<InboxService>();
        var _mediator = scoped.ServiceProvider.GetRequiredService<IMediator>();
        var assembly = typeof(IAssemblyMarker).Assembly;

        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await _inboxService.GetUnProcessedMessagesAsync(stoppingToken);
            foreach (var message in messages)
            {
                var type = assembly.GetType(message.MessageType);

                if (type is null)
                {
                    _logger.LogError("Failed to find the type '{MessageType}' in the assembly. Skipping message processing.", message.MessageType);
                    continue;
                }

                var msg = JsonSerializer.Deserialize(message.Content, type);
                if (msg is INotification request)
                {
                    await _mediator.Publish(request, stoppingToken);
                }

                await _inboxService.ProcessMessagesAsync(message, stoppingToken);
            }

            await Task.Delay(1000);
        }
    }
}
