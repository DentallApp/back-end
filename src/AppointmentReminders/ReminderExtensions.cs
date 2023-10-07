namespace DentallApp.Features.AppointmentReminders;

public static class ReminderExtensions
{
    public static IServiceCollection AddReminderServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<SendReminderJob>(configuration["REMINDER_CRON_EXPR"]);
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.AddScoped<GetScheduledAppointmentsQuery>();
        return services;
    }

    private static IServiceCollectionQuartzConfigurator AddJobAndTrigger<TJob>(
        this IServiceCollectionQuartzConfigurator configurator, 
        string cronExpression) where TJob : IJob
    {
        var jobName = typeof(TJob).Name;
        var jobKey  = new JobKey(jobName);
        configurator.AddJob<TJob>(options => options.WithIdentity(jobKey));
        configurator.AddTrigger(options => options
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronExpression));

        return configurator;
    }
}
