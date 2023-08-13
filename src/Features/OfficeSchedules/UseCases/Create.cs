namespace DentallApp.Features.OfficeSchedules.UseCases;

public class CreateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public int OfficeId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
}

public static class CreateOfficeScheduleMapper
{
    public static OfficeSchedule MapToOfficeSchedule(this CreateOfficeScheduleRequest request)
    {
        return new()
        {
            WeekDayId = request.WeekDayId,
            OfficeId  = request.OfficeId,
            StartHour = request.StartHour,
            EndHour   = request.EndHour
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

    public async Task<Response<InsertedIdDto>> Execute(ClaimsPrincipal currentEmployee, CreateOfficeScheduleRequest request)
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
