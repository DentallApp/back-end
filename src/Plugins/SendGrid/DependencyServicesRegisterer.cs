[assembly: Plugin(typeof(DependencyServicesRegisterer))]

namespace Plugin.SendGrid;

public class DependencyServicesRegisterer : IDependencyServicesRegisterer
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var settings = new EnvBinder().Bind<SendGridSettings>();
        services.AddSendGrid(options => options.ApiKey = settings.SendGridApiKey);
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton(settings);
    }
}
