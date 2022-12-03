namespace DentallApp.Extensions;

public static class QuartzJobs
{
    public static IServiceCollection AddQuartzJobs(this IServiceCollection services, AppSettings settings)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var reminderJobKey = new JobKey(nameof(ReminderJob));
            q.AddJob<ReminderJob>(options => options.WithIdentity(reminderJobKey));
            q.AddTrigger(options => options
                .ForJob(reminderJobKey)
                .WithCronSchedule(settings.ReminderCronExpr));
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        return services;
    }
}
