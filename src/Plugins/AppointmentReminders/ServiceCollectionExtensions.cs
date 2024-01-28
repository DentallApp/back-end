namespace DentallApp.Features.AppointmentReminders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReminderServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger(configuration["REMINDER_CRON_EXPR"]);
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.AddScoped<GetScheduledAppointmentsQuery>();
        return services;
    }

    private static IServiceCollectionQuartzConfigurator AddJobAndTrigger(
        this IServiceCollectionQuartzConfigurator configurator, 
        string cronExpression)
    {
        var jobName = typeof(SendReminderJob).Name;
        var jobKey  = new JobKey(jobName);
        configurator.AddJob<SendReminderJob>(options => options.WithIdentity(jobKey));
        configurator.AddTrigger(options => options
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronExpression));

        return configurator;
    }
}
