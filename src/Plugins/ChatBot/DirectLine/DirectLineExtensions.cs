namespace Plugin.ChatBot.DirectLine;

public static class DirectLineExtensions
{
    public static IServiceCollection AddDirectLineService(this IServiceCollection services)
    {
        var settings = new EnvBinder()
                           .AllowBindNonPublicProperties()
                           .Bind<DirectLineSettings>();

        services.AddSingleton(settings);
        var serviceName = settings.GetServiceName();
        services.AddHttpClient(
            name: serviceName, 
            httpClient => httpClient.BaseAddress = new Uri(settings.GetDirectLineBaseUrl())
        );

        services.AddTransient(
            serviceType:        typeof(DirectLineService),
            implementationType: Type.GetType(typeName: GetServiceNameWithNamespace(serviceName))
        );
        return services;
    }

    private static string GetServiceNameWithNamespace(string serviceName)
        => $"{typeof(DirectLineService).Namespace}.{serviceName}";
}
