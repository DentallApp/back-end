[assembly: Plugin(typeof(DependencyServicesRegisterer))]

namespace Plugin.IdentityDocument.Ecuador;

public class DependencyServicesRegisterer : IDependencyServicesRegisterer
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IIdentityDocumentValidator, IdentityDocumentValidator>();
    }
}
