namespace DentallApp.Features.EmployeeSchedules.UseCases;

public class CreateEmployeeScheduleRequest
{
    public int EmployeeId { get; init; }
    public int WeekDayId { get; init; }
    public TimeSpan? MorningStartHour { get; init; }
    public TimeSpan? MorningEndHour { get; init; }
    public TimeSpan? AfternoonStartHour { get; init; }
    public TimeSpan? AfternoonEndHour { get; init; }

    public EmployeeSchedule MapToEmployeeSchedule()
    {
        return new()
        {
            EmployeeId         = EmployeeId,
            WeekDayId          = WeekDayId,
            MorningStartHour   = MorningStartHour,
            MorningEndHour     = MorningEndHour,
            AfternoonStartHour = AfternoonStartHour,
            AfternoonEndHour   = AfternoonEndHour
        };
    }
}

public class CreateEmployeeScheduleUseCase
{
    private readonly AppDbContext _context;

    public CreateEmployeeScheduleUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> ExecuteAsync(CreateEmployeeScheduleRequest request)
    {
        var employeeSchedule = request.MapToEmployeeSchedule();
        _context.Add(employeeSchedule);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = employeeSchedule.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
