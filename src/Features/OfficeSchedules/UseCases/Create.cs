namespace DentallApp.Features.OfficeSchedules.UseCases;

public class CreateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public int OfficeId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }

    public OfficeSchedule MapToOfficeSchedule()
    {
        return new()
        {
            WeekDayId = WeekDayId,
            OfficeId  = OfficeId,
            StartHour = StartHour,
            EndHour   = EndHour
        };
    }
}

public class CreateOfficeScheduleUseCase
{
    private readonly AppDbContext _context;

    public CreateOfficeScheduleUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> ExecuteAsync(ClaimsPrincipal currentEmployee, CreateOfficeScheduleRequest request)
    {
        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return new Response<InsertedIdDto>(OfficeNotAssignedMessage);

        var officeSchedule = request.MapToOfficeSchedule();
        _context.Add(officeSchedule);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = officeSchedule.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
