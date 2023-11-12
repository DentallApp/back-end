namespace DentallApp.HostApplication;

public static class Startup
{
    /// <summary>
    /// Initializes the initial code of each plugin.
    /// </summary>
    public static void InitializePlugins(this WebApplicationBuilder builder)
    {
        var configuration = new CPluginEnvConfiguration();
        PluginLoader.SetConfiguration(configuration);
        foreach (var pluginStartup in PluginLoader.Load<IPluginStartup>())
        {
            pluginStartup.ConfigureWebApplicationBuilder(builder);
        }

        IEnumerable<IModelCreating> models = PluginLoader.Load<IModelCreating>();
        builder.Services.AddSingleton(models);
    }
}
