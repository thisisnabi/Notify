namespace Notify.Features.Email.REST.SendSms;

public class SendSmsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/email", async (SendEmailRequest requset, IMediator mediator) =>
        {
            var notify = new SendEmailMessage(requset.MessageId, requset.To,requset.Subject, requset.Body);
            await mediator.Publish(notify);
        }).WithTags("Email");
    }
}
