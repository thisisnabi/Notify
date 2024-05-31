namespace Notify.Common.Inbox;

public static class InboxConfigure
{
    public static IServiceCollection ConfigureApplicationInbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InboxService>();
        services.AddHostedService<InboxProcessBackgroundSerice>();

        var appSettings = configuration.Get<AppSettings>();

        services.AddDbContext<InboxDbContext>(options =>
        {
            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            var svcDbContext = appSettings.SvcDbContext;
            options.UseMongoDB(svcDbContext.Host, svcDbContext.DatabaseName);
        });

        return services;
    }
}
