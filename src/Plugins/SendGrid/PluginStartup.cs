[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.SendGrid;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        var settings = new EnvBinder().Bind<SendGridSettings>();
        builder.Services.AddSendGrid(options => options.ApiKey = settings.SendGridApiKey);
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddSingleton(settings);
    }
}
