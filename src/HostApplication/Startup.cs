namespace DentallApp.HostApplication;

public static class Startup
{
    /// <summary>
    /// Initializes the initial code of each plugin.
    /// </summary>
    public static void InitializePlugins(this WebApplicationBuilder builder)
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
