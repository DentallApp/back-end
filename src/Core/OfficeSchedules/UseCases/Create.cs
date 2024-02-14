namespace DentallApp.Core.OfficeSchedules.UseCases;

public class CreateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public int OfficeId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }

    public OfficeSchedule MapToOfficeSchedule() => new()
    {
        WeekDayId = WeekDayId,
        OfficeId  = OfficeId,
        StartHour = StartHour,
        EndHour   = EndHour
    };
}

public class CreateOfficeScheduleUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(ClaimsPrincipal currentEmployee, CreateOfficeScheduleRequest request)
    {
        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        var officeSchedule = request.MapToOfficeSchedule();
        context.Add(officeSchedule);
        await context.SaveChangesAsync();
        return Result.CreatedResource(officeSchedule.Id);
    }
}
