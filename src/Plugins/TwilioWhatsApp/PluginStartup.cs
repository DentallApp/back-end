[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.Twilio.WhatsApp;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        var settings = new EnvBinder().Bind<TwilioSettings>();
        builder.Services.AddSingleton<IInstantMessaging, WhatsAppMessaging>();
        builder.Services.AddSingleton(settings);
    }
}
