namespace Notify.Features.Sms;

public static class SmsFeatureConfigure
{
    public static IServiceCollection ConfigureSmsFeature(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<InquirySmsBackgroundService>();
        services.AddScoped<SmsService>();

        services.AddKeyedScoped<ISmsProvider, ProviderASmsProvider>("ProviderA");
        services.AddKeyedScoped<ISmsProvider, ProviderBSmsProvider>("ProviderB");

        var appSettings = configuration.Get<AppSettings>();

        services.AddDbContext<SmsDbContext>(options =>
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
