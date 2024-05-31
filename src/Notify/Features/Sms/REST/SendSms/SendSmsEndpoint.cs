namespace Notify.Features.Sms.REST.SendSms;

public class SendSmsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/sms", async (SendSmsRequest requset, IMediator mediator) =>
        {
            var notify = new SendSmsMessage(requset.MessageId, requset.Mobile, requset.Message);
            await mediator.Publish(notify);
        }).WithTags("Sms");
    }
}
