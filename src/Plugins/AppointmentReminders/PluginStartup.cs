[assembly: Plugin(typeof(PluginStartup))]

namespace Plugin.AppointmentReminders;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddReminderServices();
    }
}
