using System.Reflection;

namespace Notify.Features.Sms;

public static class SmsFeatureConfigure
{
    public static IServiceCollection ConfigureSmsFeature(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<InquirySmsBackgroundService>();
        services.AddScoped<SmsService>();

        // IoC of ISmsProvider
        // This is dynamic DI based on ISmsProvider
        var smsProviderType = typeof(ISmsProvider);
        var types = Assembly.GetExecutingAssembly().GetTypes()
                     .Where(t => smsProviderType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        foreach (var type in types)
            services.AddScoped(smsProviderType, type);
        // end


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
