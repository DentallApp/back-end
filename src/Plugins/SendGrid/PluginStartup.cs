[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.SendGrid;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        var apiKey = builder.Configuration["SENDGRID_API_KEY"];
        builder.Services.AddSendGrid(options => options.ApiKey = apiKey);
        builder.Services.AddScoped<IEmailService, EmailService>();
    }
}
