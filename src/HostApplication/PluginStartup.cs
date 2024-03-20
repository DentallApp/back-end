namespace DentallApp.HostApplication;

public static class PluginStartup
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var envConfiguration = new CPluginEnvConfiguration();
        PluginLoader.Load(envConfiguration);
        var dependencyServicesRegisterers = TypeFinder.FindSubtypesOf<IDependencyServicesRegisterer>();
        foreach (var dependencyServicesRegisterer in dependencyServicesRegisterers)
            dependencyServicesRegisterer.RegisterServices(builder.Services, builder.Configuration);

        builder
            .Services
            .AddSubtypesOf<IEntityTypeConfigurator>(ServiceLifetime.Transient);

        // These services are only added when no plugin registers its own implementation.
        builder.Services.TryAddSingleton<IEmailService, FakeEmailService>();
        builder.Services.TryAddSingleton<IInstantMessaging, FakeInstantMessaging>();
    }
}
