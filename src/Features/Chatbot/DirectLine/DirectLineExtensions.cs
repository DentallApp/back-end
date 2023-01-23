namespace DentallApp.Features.Chatbot.DirectLine;

public static class DirectLineExtensions
{
    public static IServiceCollection AddDirectLineService(this IServiceCollection services)
    {
        var settings = new EnvBinder()
                           .AllowBindNonPublicProperties()
                           .Bind<DirectLineSettings>();

        services.AddSingleton(settings);
        var providerName = settings.GetProviderName();
        services.AddHttpClient(
            name: providerName, 
            httpClient => httpClient.BaseAddress = new Uri(settings.GetDirectLineBaseUrl())
        );

        services.AddTransient(
            serviceType:        typeof(DirectLineService),
            implementationType: Type.GetType(GetProviderTypeName(providerName))
        );
        return services;
    }

    private static string GetProviderTypeName(string providerName)
        => $"{typeof(DirectLineService).Namespace}.{providerName}";
}
