namespace DentallApp.Features.AppointmentReminders;

public class SendReminderJob(
    ILogger<SendReminderJob> logger,
    AppSettings settings,
    IServiceProvider serviceProvider,
    IInstantMessaging instantMessaging,
    IDateTimeService dateTimeService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Sending appointment reminder.");
        var currentDateAndTime    = dateTimeService.Now;
        using var scope           = serviceProvider.CreateScope();
        var query                 = scope.ServiceProvider.GetRequiredService<GetScheduledAppointmentsQuery>();
        var scheduledAppointments = query.Execute(settings.ReminderTimeInAdvance, currentDateAndTime.Date);
        var businessName          = EnvReader.Instance[AppSettings.BusinessName];
        foreach (var appointment in scheduledAppointments)
        {
            var message = string.Format(
                MessageTemplates.AppointmentReminderMessageTemplate,
                appointment.PatientName,
                appointment.Date.GetDateInSpanishFormat(),
                appointment.StartHour.GetHourWithoutSeconds(),
                businessName,
                appointment.DentistName
            );
            await instantMessaging.SendMessageAsync(appointment.PatientCellPhone, message);
        }
    }
}
