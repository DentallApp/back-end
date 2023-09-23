namespace DentallApp.Features.AppointmentReminders;

public class SendReminderJob : IJob
{
    private readonly ILogger<SendReminderJob> _logger;
    private readonly AppSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly IInstantMessaging _instantMessaging;
    private readonly IDateTimeService _dateTimeService;

    public SendReminderJob(
        ILogger<SendReminderJob> logger, 
        AppSettings settings,
        IServiceProvider serviceProvider,
        IInstantMessaging instantMessaging,
        IDateTimeService dateTimeService)
    {
        _settings = settings;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _instantMessaging = instantMessaging;
        _dateTimeService = dateTimeService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Sending appointment reminder.");
        var currentDateAndTime    = _dateTimeService.Now;
        using var scope           = _serviceProvider.CreateScope();
        var useCase               = scope.ServiceProvider.GetRequiredService<GetScheduledAppointmentsUseCase>();
        var scheduledAppointments = useCase.Execute(_settings.ReminderTimeInAdvance, currentDateAndTime.Date);
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
            await _instantMessaging.SendMessageAsync(appointment.PatientCellPhone, message);
        }
    }
}
