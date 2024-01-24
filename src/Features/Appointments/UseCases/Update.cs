namespace DentallApp.Features.Appointments.UseCases;

public class UpdateAppointmentRequest
{
    public int StatusId { get; init; }
}

public class UpdateAppointmentUseCase(DbContext context, IDateTimeService dateTimeService)
{
    public async Task<Result> ExecuteAsync(int id, ClaimsPrincipal currentEmployee, UpdateAppointmentRequest request)
    {
        var appointment = await context.Set<Appointment>()
            .Where(appointment => appointment.Id == id)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return Result.NotFound(ResourceNotFoundMessage);

        if (appointment.AppointmentStatusId == AppointmentStatusId.Canceled)
            return Result.Conflict(AppointmentIsAlreadyCanceledMessage);

        if (dateTimeService.Now.Date > appointment.Date)
            return Result.Forbidden(AppointmentCannotBeUpdatedForPreviousDaysMessage);

        if (currentEmployee.IsOnlyDentist() && appointment.DentistId != currentEmployee.GetEmployeeId())
            return Result.Forbidden(AppointmentNotAssignedMessage);

        if (currentEmployee.IsNotInOffice(appointment.OfficeId))
            return Result.Forbidden(OfficeNotAssignedMessage);

        appointment.AppointmentStatusId = request.StatusId;
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
