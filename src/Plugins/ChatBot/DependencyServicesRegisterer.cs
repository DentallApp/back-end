[assembly: Plugin(typeof(DependencyServicesRegisterer))]

namespace Plugin.ChatBot;

public class DependencyServicesRegisterer : IDependencyServicesRegisterer
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddBotServices(configuration);
    }
}
