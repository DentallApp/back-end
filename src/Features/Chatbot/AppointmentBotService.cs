namespace DentallApp.Features.Chatbot;

public class AppointmentBotService : IAppointmentBotService
{
    private readonly IServiceProvider _serviceProvider;

    public AppointmentBotService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetDentalServicesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetDentistsByOfficeIdAsync(int officeId)
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetDentistsByOfficeIdAsync(officeId);
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetOfficesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile)
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetPatientsAsync(userProfile);
    }

    public async Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto)
    {
        using var scope = _serviceProvider.CreateScope();
        var availabilityService = scope.ServiceProvider.GetRequiredService<IAvailabilityService>();
        return await availabilityService.GetAvailableHoursAsync(availableTimeRangeDto);
    }

    public async Task<Response> CreateScheduledAppointmentAsync(AppointmentInsertDto appointment)
    {
        using var scope = _serviceProvider.CreateScope();
        var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
        return await appointmentService.CreateAppointmentAsync(appointment);
    }

    public async Task<SpecificTreatmentRangeToPayDto> GetRangeToPayAsync(int dentalServiceId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISpecificTreatmentRepository>();
        return await repository.GetTreatmentWithRangeToPayAsync(dentalServiceId);
    }

    public async Task<string> GetDentistScheduleAsync(int dentistId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IEmployeeScheduleRepository>();
        var weekDays = (await repository.GetEmployeeScheduleWithOnlyWeekDayAsync(dentistId)) as List<WeekDayDto>;
        return WeekDayFormat.GetWeekDaysFormat(weekDays);
    }
}
