namespace Notify.Features.Sms.REST.GetSmsTrace;

public class GetEmailTraceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/email/{message_id:required:guid}",
            async ([FromRoute(Name = "message_id")] Guid MessageId,
                   EmailDbContext dbContext,
                   CancellationToken cancellationToken) =>
        {
            var smsTrace = await dbContext.EmailTraces.FirstOrDefaultAsync(x => x.MessageId == MessageId, cancellationToken);

            if (smsTrace is null)
                return Results.NotFound();

            return Results.Ok(new
            {
                smsTrace.To,
                smsTrace.Subject,
                smsTrace.CreatedOn,
                smsTrace.Status
            });
        }).WithTags("Email");
    }
}
