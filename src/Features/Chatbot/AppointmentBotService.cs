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

    public async Task<Response<IEnumerable<AvailableTimeRangeResponse>>> GetAvailableHoursAsync(AvailableTimeRangeRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<GetAvailableHoursUseCase>();
        return await useCase.Execute(request);
    }

    public async Task<Response<InsertedIdDto>> CreateScheduledAppointmentAsync(CreateAppointmentRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<CreateAppointmentUseCase>();
        return await useCase.Execute(request);
    }

    public async Task<RangeToPayResponse> GetRangeToPayAsync(int dentalServiceId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISpecificTreatmentRepository>();
        return await repository.GetRangeToPay(dentalServiceId);
    }

    public async Task<string> GetDentistScheduleAsync(int dentistId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IEmployeeScheduleRepository>();
        var weekDays = (await repository.GetOnlyWeekDays(dentistId)) as List<WeekDayResponse>;
        return WeekDayFormat.GetWeekDaysFormat(weekDays);
    }
}
