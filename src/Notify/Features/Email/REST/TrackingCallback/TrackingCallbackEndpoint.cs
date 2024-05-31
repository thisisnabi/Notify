namespace Notify.Features.Email.REST.TrackingCallback;

public class TrackingCallbackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/email/tracking/{track_id:required}", async (
            [FromRoute(Name = "track_id")] string trackId, 
            ILogger<TrackingCallbackEndpoint> logger,
            EmailDbContext dbContext, 
            CancellationToken cancellationToken) =>
        {
            var emailTrace = await dbContext.EmailTraces.FirstOrDefaultAsync(x => x.TrackerId == trackId, cancellationToken);

            if(emailTrace is null)
            {
                logger.LogWarning("Email trace with TrackerId {TrackId} not found.", trackId);
                return Results.BadRequest();
            };

            emailTrace.Status = EmailTraceStatus.Opened;
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok();
        }).WithTags("Email");
    }
}
