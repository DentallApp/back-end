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
        var botQuery = scope.ServiceProvider.GetRequiredService<IBotQueries>();
        return await botQuery.GetDentalServicesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId)
    {
		using var scope = _serviceProvider.CreateScope();
        var botQuery = scope.ServiceProvider.GetRequiredService<IBotQueries>();
        return await botQuery.GetDentistsAsync(officeId, specialtyId);
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var botQuery = scope.ServiceProvider.GetRequiredService<IBotQueries>();
        return await botQuery.GetOfficesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile)
    {
		using var scope = _serviceProvider.CreateScope();
        var botQuery = scope.ServiceProvider.GetRequiredService<IBotQueries>();
        return await botQuery.GetPatientsAsync(userProfile);
    }

    public async Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto)
    {
        using var scope = _serviceProvider.CreateScope();
        var availabilityService = scope.ServiceProvider.GetRequiredService<IAvailabilityService>();
        return await availabilityService.GetAvailableHoursAsync(availableTimeRangeDto);
    }

    public async Task<Response<DtoBase>> CreateScheduledAppointmentAsync(AppointmentInsertDto appointment)
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
