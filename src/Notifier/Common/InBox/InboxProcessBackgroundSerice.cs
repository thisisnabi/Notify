using MediatR;
using Newtonsoft.Json;

namespace Notifier.Common.InBox;

public class InboxProcessBackgroundSerice(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

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
                    // log error
                }

                var msg = JsonConvert.DeserializeObject(message.Content, type);
                if (msg is INotification notify)
                {
                    await _mediator.Publish(notify, stoppingToken);
                }

                await _inboxService.ProcessMessagesAsync(message, stoppingToken);
            }

            await Task.Delay(1000);
        }
    }
}
