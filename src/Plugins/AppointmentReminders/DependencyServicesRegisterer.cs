[assembly: Plugin(typeof(DependencyServicesRegisterer))]

namespace Plugin.AppointmentReminders;

public class DependencyServicesRegisterer : IDependencyServicesRegisterer
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddReminderServices();
    }
}
