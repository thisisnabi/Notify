using MediatR;

namespace Notifier.Common;

public record BaseMessage(Guid MessageId) : INotification;