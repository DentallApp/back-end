namespace DentallApp.Core;

public static class CoreServicesExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAvailabilityQueries, AvailabilityQueries>();

        return services;
    }
}
