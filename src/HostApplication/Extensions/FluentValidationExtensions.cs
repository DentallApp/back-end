namespace DentallApp.HostApplication.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssemblies(PluginLoader.Assemblies, ServiceLifetime.Transient)
            .AddValidatorsFromAssembly(typeof(CoreServicesExtensions).Assembly, ServiceLifetime.Transient); 
        
        return services;
    }
}
