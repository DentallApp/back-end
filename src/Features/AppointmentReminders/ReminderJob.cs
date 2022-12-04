namespace DentallApp.Features.AppointmentReminders;

public class ReminderJob : IJob
{
    private readonly ILogger<ReminderJob> _logger;
    private readonly AppSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly IInstantMessaging _instantMessaging;
    private const string TemplateMessage = 
        "Estimado usuario {0}, le recordamos que el día {1} a las {2} tiene una cita agendada en el consultorio {3} con el odontólogo {4}";

    public ReminderJob(ILogger<ReminderJob> logger, 
                              AppSettings settings,
                              IServiceProvider serviceProvider,
                              IInstantMessaging instantMessaging)
    {
        _settings = settings;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _instantMessaging = instantMessaging;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Sending appointment reminder.");
        var currentDateAndTime = DateTime.Now;
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IAppointmentReminderRepository>();
        var scheduledAppointments = repository.GetScheduledAppointments(_settings.ReminderTimeInAdvance, currentDateAndTime.Date);
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        foreach (var appointmentDto in scheduledAppointments)
        {
            var message = string.Format(TemplateMessage,
                                        appointmentDto.PatientName,
                                        appointmentDto.Date.GetDateInSpanishFormat(),
                                        appointmentDto.StartHour.GetHourWithoutSeconds(),
                                        businessName,
                                        appointmentDto.DentistName);
            _instantMessaging.SendMessage(appointmentDto.PatientCellPhone, message);
        }
        return Task.CompletedTask;
    }
}
