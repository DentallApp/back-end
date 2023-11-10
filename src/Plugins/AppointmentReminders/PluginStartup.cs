[assembly: Plugin(typeof(PluginStartup))]

namespace DentallApp.Features.AppointmentReminders;

public class PluginStartup : IPluginStartup
{
    public void ConfigureWebApplicationBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddReminderServices(builder.Configuration);
    }
}
