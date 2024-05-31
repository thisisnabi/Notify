namespace Notify.Common.Abstractions;

public abstract record IntegrationMessage(Guid MessageId) : INotification;