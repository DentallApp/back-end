namespace DentallApp.HostApplication;

public static class PluginStartup
{
    /// <summary>
    /// Configures the plugins.
    /// </summary>
    public static void Configure(WebApplicationBuilder builder)
    {
        var envConfiguration = new CPluginEnvConfiguration();
        PluginLoader.Load(envConfiguration);
        var dependencyServicesRegisterers = TypeFinder.FindSubtypesOf<IDependencyServicesRegisterer>();
        foreach (var dependencyServicesRegisterer in dependencyServicesRegisterers)
            dependencyServicesRegisterer.RegisterServices(builder.Services, builder.Configuration);

        var modelCreatings = TypeFinder.FindSubtypesOf<IModelCreating>();
        builder.Services.AddSingleton(modelCreatings);

        // These services are only added when no plug-in registers its own implementation.
        builder.Services.TryAddSingleton<IEmailService, FakeEmailService>();
        builder.Services.TryAddSingleton<IInstantMessaging, FakeInstantMessaging>();
    }
}
