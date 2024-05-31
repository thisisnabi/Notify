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
}
