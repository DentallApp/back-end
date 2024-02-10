namespace Plugin.AppointmentReminders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReminderServices(this IServiceCollection services)
    {
        var settings = new EnvBinder().Bind<ReminderSettings>();
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger(settings.ReminderCronExpr);
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.AddScoped<GetScheduledAppointmentsQuery>();
        services.AddSingleton(settings);
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
