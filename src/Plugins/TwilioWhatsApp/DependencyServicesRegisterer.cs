[assembly: Plugin(typeof(DependencyServicesRegisterer))]

namespace Plugin.Twilio.WhatsApp;

public class DependencyServicesRegisterer : IDependencyServicesRegisterer
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var settings = new EnvBinder().Bind<TwilioSettings>();
        services.AddSingleton<IInstantMessaging, WhatsAppMessaging>();
        services.AddSingleton(settings);
    }
}
