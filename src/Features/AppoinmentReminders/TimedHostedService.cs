namespace DentallApp.Features.AppoinmentReminders;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly AppSettings _settings ;
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IInstantMessaging _instantMessaging;
    private Timer _timer;
    private const string TemplateMessage = 
        "Estimado usuario {0}, le recordamos que el día {1} a las {2} tiene una cita agendada en el consultorio {3} con el odontólogo {4}";

    public TimedHostedService(ILogger<TimedHostedService> logger, 
                              AppSettings settings,
                              IServiceProvider serviceProvider,
                              IInstantMessaging instantMessaging)
    {
        _settings = settings;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _instantMessaging = instantMessaging;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        var dueTime = TimeSpan.FromMilliseconds(_settings.ReminderDueTime * 60000);
        var period  = TimeSpan.FromMilliseconds(_settings.ReminderPeriod * 60000);
        _timer = new Timer(DoWork, null, dueTime, period);
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _logger.LogInformation("Sending appointment reminder.");
        var currentDateAndTime = DateTime.Now;
        if (currentDateAndTime.Hour < _settings.ReminderHourMin || 
            currentDateAndTime.Hour > _settings.ReminderHourMax) return;
        using var scope = _serviceProvider.CreateScope();
        var repository           = scope.ServiceProvider.GetRequiredService<IAppoinmentReminderRepository>();
        var scheduledAppoinments = repository.GetScheduledAppoinments(_settings.ReminderTimeInAdvance, currentDateAndTime.Date);
        var businessName         = EnvReader.Instance[AppSettings.BusinessName];

        foreach (var appoinmentDto in scheduledAppoinments)
        {
            var message = string.Format(TemplateMessage,
                                        appoinmentDto.PatientName,
                                        appoinmentDto.Date.GetDateInSpanishFormat(),
                                        appoinmentDto.StartHour.GetHourWithoutSeconds(),
                                        businessName,
                                        appoinmentDto.DentistName);
            _instantMessaging.SendMessage(appoinmentDto.PatientCellPhone, message);
        }
        if (scheduledAppoinments.Any())
        {
            var appoinmentsId = scheduledAppoinments.Select(appoinment => appoinment.AppoinmentId);
            repository.UpdateScheduledAppoinments(appoinmentsId);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
