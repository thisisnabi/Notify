namespace Notify.Common.Inbox;

[Collection("inbox_messages")]
public class InboxMessage
{

    public ObjectId Id { get; set; }

    public required Guid MessageId { get; set; }

    public required string MessageType { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ProcessedOn { get; set; }

    public bool Processed { get; set; }

    public void Process()
    {
        Processed = true;
        ProcessedOn = DateTime.UtcNow;
    }

    public static InboxMessage Create<TModel>(TModel model) where TModel : IntegrationMessage
    {
        return new InboxMessage
        {
            Content = JsonSerializer.Serialize(model),
            CreatedOn = DateTime.Now,
            Processed = false,
            MessageType = typeof(TModel).FullName!,
            MessageId = model.MessageId
        };
    }
}
