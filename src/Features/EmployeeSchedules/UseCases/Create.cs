namespace DentallApp.Features.EmployeeSchedules.UseCases;

public class CreateEmployeeScheduleRequest
{
    public int EmployeeId { get; init; }
    public int WeekDayId { get; init; }
    public TimeSpan? MorningStartHour { get; init; }
    public TimeSpan? MorningEndHour { get; init; }
    public TimeSpan? AfternoonStartHour { get; init; }
    public TimeSpan? AfternoonEndHour { get; init; }
}

public static class CreateEmployeeScheduleMapper
{
    public static EmployeeSchedule MapToEmployeeSchedule(this CreateEmployeeScheduleRequest request)
    {
        return new()
        {
            EmployeeId         = request.EmployeeId,
            WeekDayId          = request.WeekDayId,
            MorningStartHour   = request.MorningStartHour,
            MorningEndHour     = request.MorningEndHour,
            AfternoonStartHour = request.AfternoonStartHour,
            AfternoonEndHour   = request.AfternoonEndHour
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

    public async Task<Response<InsertedIdDto>> Execute(CreateEmployeeScheduleRequest request)
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
