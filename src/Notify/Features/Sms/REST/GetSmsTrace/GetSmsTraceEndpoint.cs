namespace Notify.Features.Sms.REST.GetSmsTrace;

public class GetSmsTraceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/sms/{message_id:required:guid}",
            async ([FromRoute(Name = "message_id")] Guid MessageId,
                   SmsDbContext dbContext,
                   CancellationToken cancellationToken) =>
        {
            var smsTrace = await dbContext.SmsTraces.FirstOrDefaultAsync(x => x.MessageId == MessageId, cancellationToken);

            if (smsTrace is null)
                return Results.NotFound();

            return Results.Ok(new
            {
                smsTrace.Mobile,
                smsTrace.Message,
                smsTrace.CreatedOn,
                smsTrace.Status
            });
        }).WithTags("Sms");
    }
}
