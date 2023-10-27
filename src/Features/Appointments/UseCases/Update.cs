namespace DentallApp.Features.Appointments.UseCases;

public class UpdateAppointmentRequest
{
    public int StatusId { get; init; }
}

public class UpdateAppointmentUseCase
{
    private readonly DbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public UpdateAppointmentUseCase(DbContext context, IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result> ExecuteAsync(int id, ClaimsPrincipal currentEmployee, UpdateAppointmentRequest request)
    {
        var appointment = await _context.Set<Appointment>()
            .Where(appointment => appointment.Id == id)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return Result.NotFound(ResourceNotFoundMessage);

        if (appointment.AppointmentStatusId == AppointmentStatusId.Canceled)
            return Result.Failure(AppointmentIsAlreadyCanceledMessage);

        if (_dateTimeService.Now.Date > appointment.Date)
            return Result.Failure(AppointmentCannotBeUpdatedForPreviousDaysMessage);

        if (currentEmployee.IsOnlyDentist() && appointment.DentistId != currentEmployee.GetEmployeeId())
            return Result.Forbidden(AppointmentNotAssignedMessage);

        if (currentEmployee.IsNotInOffice(appointment.OfficeId))
            return Result.Forbidden(OfficeNotAssignedMessage);

        appointment.AppointmentStatusId = request.StatusId;
        await _context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
