namespace DentallApp.Shared.Interfaces;

/// <summary>
/// Base type for initializing services used by a plugin.
/// </summary>
public interface IDependencyServicesRegisterer
{
    /// <summary>
    /// Register services into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add the services to.
    /// </param>
    /// <param name="configuration">
    /// The set of key/value configuration properties.
    /// </param>
    void RegisterServices(IServiceCollection services, IConfiguration configuration);
}
