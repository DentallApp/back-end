[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.Twilio.WhatsApp;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IInstantMessaging, WhatsAppMessaging>();
    }
}
