[assembly: Plugin(typeof(PluginStartup))]

namespace Plugin.ChatBot;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddBotServices(builder.Configuration);
    }
}
