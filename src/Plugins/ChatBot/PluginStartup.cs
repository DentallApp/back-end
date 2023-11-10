[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.Features.ChatBot;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddBotServices(builder.Configuration);
    }
}
