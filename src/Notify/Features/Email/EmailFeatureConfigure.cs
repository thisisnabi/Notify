namespace Notify.Features.Email;

public static class EmailFeatureConfigure
{
    public static IServiceCollection ConfigureEmailFeature(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<EmailService>();

        var appSettings = configuration.Get<AppSettings>();

        services.AddDbContext<EmailDbContext>(options =>
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
