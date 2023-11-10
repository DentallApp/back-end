namespace DentallApp.Shared.Plugin.Contracts;

/// <summary>
/// Represents the entry point of the plugin.
/// </summary>
public interface IPluginStartup
{
    void ConfigureWebApplicationBuilder(WebApplicationBuilder builder);
}
