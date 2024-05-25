using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Notifier.Common.InBox;

[Collection("inbox_messages")]
public class InboxMessage
{

    public ObjectId Id { get; set; }

    public required Guid MessageId { get; set; }

    public required string MessageType { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Processed { get; set; }

    public void Process() => Processed = true;


    public static InboxMessage Create<TModel>(TModel model)
        where TModel : BaseMessage
    {
        return new InboxMessage
        {
            Content = JsonConvert.SerializeObject(model),
            CreatedOn = DateTime.Now,
            Processed = false,
            MessageType = typeof(TModel).FullName!,
            MessageId = model.MessageId
        };
    }
}
