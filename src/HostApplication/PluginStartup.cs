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
        foreach (var pluginStartup in TypeFinder.FindSubtypesOf<IPluginStartup>())
        {
            pluginStartup.ConfigureWebApplicationBuilder(builder);
        }

        IEnumerable<IModelCreating> models = TypeFinder.FindSubtypesOf<IModelCreating>();
        builder.Services.AddSingleton(models);
    }
}
