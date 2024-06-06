namespace Notify.Features.Email.REST.GetEmailTrace;

public class GetEmailTraceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/email/{message_id:required:guid}",
            async ([FromRoute(Name = "message_id")] Guid MessageId,
                   EmailDbContext dbContext,
                   CancellationToken cancellationToken) =>
        {
            var emailTrace = await dbContext.EmailTraces.FirstOrDefaultAsync(x => x.MessageId == MessageId, cancellationToken);

            if (emailTrace is null)
                return Results.NotFound();

            return Results.Ok(new
            {
                emailTrace.To,
                emailTrace.Subject,
                emailTrace.CreatedOn,
                emailTrace.Status
            });
        }).WithTags("Email");
    }
}
