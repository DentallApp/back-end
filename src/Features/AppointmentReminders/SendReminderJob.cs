using DentallApp.Features.AppointmentReminders.Queries;

namespace DentallApp.Features.AppointmentReminders;

public class SendReminderJob : IJob
{
    private readonly ILogger<SendReminderJob> _logger;
    private readonly AppSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly IInstantMessaging _instantMessaging;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SendReminderJob(
        ILogger<SendReminderJob> logger, 
        AppSettings settings,
        IServiceProvider serviceProvider,
        IInstantMessaging instantMessaging,
        IDateTimeProvider dateTimeProvider)
    {
        _settings = settings;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _instantMessaging = instantMessaging;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Sending appointment reminder.");
        var currentDateAndTime    = _dateTimeProvider.Now;
        using var scope           = _serviceProvider.CreateScope();
        var query                 = scope.ServiceProvider.GetRequiredService<GetScheduledAppointmentsQuery>();
        var scheduledAppointments = query.Execute(_settings.ReminderTimeInAdvance, currentDateAndTime.Date);
        var businessName          = EnvReader.Instance[AppSettings.BusinessName];
        foreach (var appointment in scheduledAppointments)
        {
            var message = string.Format(
                AppointmentReminderMessageTemplate,
                appointment.PatientName,
                appointment.Date.GetDateInSpanishFormat(),
                appointment.StartHour.GetHourWithoutSeconds(),
                businessName,
                appointment.DentistName
            );
            _instantMessaging.SendMessage(appointment.PatientCellPhone, message);
        }
        return Task.CompletedTask;
    }
}
