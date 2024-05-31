namespace Notify.Common;

public static class DependencyInjections
{
    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            var assembly = typeof(IAssemblyMarker).Assembly;
            configure.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }

    public static IServiceCollection ConfigureBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.Get<AppSettings>();

        services.AddMassTransit(configure =>
        {
            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            var brokerConfig = appSettings.BrokerConfiguration;

            configure.AddConsumers(typeof(IAssemblyMarker).Assembly);

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonDeserializer();

                cfg.Host(brokerConfig.Host, hostConfigure =>
                {
                    hostConfigure.Username(brokerConfig.Username);
                    hostConfigure.Password(brokerConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
