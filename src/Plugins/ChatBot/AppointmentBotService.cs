namespace Plugin.ChatBot;

public class AppointmentBotService(IServiceProvider serviceProvider) : IAppointmentBotService
{
    private AdaptiveChoice MapToAdaptiveChoice(SchedulingResponse response)
    {
        return new AdaptiveChoice { Title = response.Title, Value = response.Value };
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
    {
		using var scope    = serviceProvider.CreateScope();
        var queries        = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var dentalServices = await queries.GetDentalServicesAsync();
        return dentalServices.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId)
    {
		using var scope = serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var dentists    = await queries.GetDentistsAsync(officeId, specialtyId);
        return dentists.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
		using var scope = serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var offices     = await queries.GetOfficesAsync();
        return offices.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(AuthenticatedUser user)
    {
		using var scope = serviceProvider.CreateScope();
        var queries     = scope.ServiceProvider.GetRequiredService<ISchedulingQueries>();
        var patients    = await queries.GetPatientsAsync(user);
        return patients.Select(MapToAdaptiveChoice).ToList();
    }

    public async Task<ListedResult<AvailableTimeRangeResponse>> GetAvailableHoursAsync(AvailableTimeRangeRequest request)
    {
        using var scope = serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<IGetAvailableHoursUseCase>();
        return await useCase.ExecuteAsync(request);
    }

    public async Task<Result<CreatedId>> CreateScheduledAppointmentAsync(CreateAppointmentRequest request)
    {
        using var scope = serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<ICreateAppointmentUseCase>();
        return await useCase.ExecuteAsync(request);
    }

    public async Task<PayRange> GetRangeToPayAsync(int dentalServiceId)
    {
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITreatmentRepository>();
        return await repository.GetRangeToPayAsync(dentalServiceId);
    }

    public async Task<string> GetDentistScheduleAsync(int dentistId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        var weekDays = await GetOnlyWeekDaysAsync(dbContext, dentistId);
        return WeekDay.ConvertToDayRange(weekDays);
    }

    private static async Task<List<WeekDay>> GetOnlyWeekDaysAsync(DbContext context, int employeeId)
    {
        var schedules = await context.Set<EmployeeSchedule>()
            .Where(employeeSchedule => employeeSchedule.EmployeeId == employeeId)
            .OrderBy(employeeSchedule => employeeSchedule.WeekDayId)
            .Select(employeeSchedule => new WeekDay
            {
                Id   = employeeSchedule.WeekDayId,
                Name = employeeSchedule.WeekDay.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return schedules;
    }
}
