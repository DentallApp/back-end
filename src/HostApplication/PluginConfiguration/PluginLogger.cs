namespace DentallApp.HostApplication.PluginConfiguration;

/// <summary>
/// Represents a type used to perform logging.
/// </summary>
public class PluginLogger
{
    /// <summary>
    /// Writes an informative log message indicating that the plugin was loaded successfully.
    /// </summary>
    /// <param name="pluginName">The plugin name.</param>
    /// <param name="categoryName">The category name for messages produced by the logger.</param>
    public static void DefaultLogInformation(string pluginName, string categoryName)
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole()
                   .SetMinimumLevel(LogLevel.Information);
        });

        ILogger logger = loggerFactory.CreateLogger(categoryName);
        logger.LogInformation("'{pluginName}' plug-in has been successfully loaded.", pluginName);
    }
}
