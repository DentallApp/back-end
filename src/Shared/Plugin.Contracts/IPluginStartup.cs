namespace DentallApp.Shared.Plugin.Contracts;

public interface IPluginStartup
{
    void ConfigureWebApplicationBuilder(WebApplicationBuilder builder);
}
