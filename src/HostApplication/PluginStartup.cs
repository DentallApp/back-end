namespace DentallApp.HostApplication;

public static class PluginStartup
{
    /// <summary>
    /// Configures the application plug-ins.
    /// </summary>
    public static void Configure(WebApplicationBuilder builder)
    {
        var envConfiguration = new CPluginEnvConfiguration();
        PluginLoader.Load(envConfiguration);
        var startups = TypeFinder.FindSubtypesOf<IPluginStartup>();
        foreach (var pluginStartup in startups)
            pluginStartup.ConfigureWebApplicationBuilder(builder);

        var modelCreatings = TypeFinder.FindSubtypesOf<IModelCreating>();
        builder.Services.AddSingleton(modelCreatings);

        // These services are only added when no plug-in registers its own implementation.
        builder.Services.TryAddSingleton<IEmailService, FakeEmailService>();
        builder.Services.TryAddSingleton<IInstantMessaging, FakeInstantMessaging>();
    }
}
