namespace DentallApp.HostApplication.Extensions;

public static class QuartzJobs
{
    public static IServiceCollection AddQuartzJobs(this IServiceCollection services, AppSettings settings)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<SendReminderJob>(settings.ReminderCronExpr);
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
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
