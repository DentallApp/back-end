namespace DentallApp.Features.Chatbot;

public class AppointmentBotService : IAppointmentBotService
{
    private readonly IServiceProvider _serviceProvider;

    public AppointmentBotService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private AdaptiveChoice MapToAdaptiveChoice(SchedulingResponse response)
    {
        return new AdaptiveChoice { Title = response.Title, Value = response.Value };
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
    {
		using var scope    = _serviceProvider.CreateScope();
        var queries        = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var dentalServices = await queries.GetDentalServicesAsync();
        return dentalServices.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId)
    {
		using var scope = _serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var dentists    = await queries.GetDentistsAsync(officeId, specialtyId);
        return dentists.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var offices     = await queries.GetOfficesAsync();
        return offices.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(AuthenticatedUser user)
    {
		using var scope = _serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var patients    = await queries.GetPatientsAsync(user);
        return patients.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<Response<IEnumerable<AvailableTimeRangeResponse>>> GetAvailableHoursAsync(AvailableTimeRangeRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<IGetAvailableHoursUseCase>();
        return await useCase.ExecuteAsync(request);
    }

    public async Task<Response<InsertedIdDto>> CreateScheduledAppointmentAsync(CreateAppointmentRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<ICreateAppointmentUseCase>();
        return await useCase.ExecuteAsync(request);
    }

    public async Task<PayRange> GetRangeToPayAsync(int dentalServiceId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITreatmentRepository>();
        return await repository.GetRangeToPayAsync(dentalServiceId);
    }

    public async Task<string> GetDentistScheduleAsync(int dentistId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IEmployeeScheduleRepository>();
        var weekDays = (await repository.GetOnlyWeekDaysAsync(dentistId)) as List<WeekDay>;
        return WeekDay.ConvertToDayRange(weekDays);
    }
}
