namespace DentallApp.Core.Appointments.UseCases;

public class UpdateAppointmentRequest
{
    public int StatusId { get; init; }
}

public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentValidator()
    {
        RuleFor(request => request.StatusId).GreaterThan(0);
    }
}

public class UpdateAppointmentUseCase(
    DbContext context, 
    IDateTimeService dateTimeService,
    ICurrentEmployee currentEmployee,
    UpdateAppointmentValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdateAppointmentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var appointment = await context.Set<Appointment>()
            .Where(appointment => appointment.Id == id)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return Result.NotFound(Messages.ResourceNotFound);

        if (appointment.AppointmentStatusId == (int)AppointmentStatus.Predefined.Canceled)
            return Result.Conflict(Messages.AppointmentIsAlreadyCanceled);

        if (dateTimeService.Now.Date > appointment.Date)
            return Result.Forbidden(Messages.AppointmentCannotBeUpdatedForPreviousDays);

        if (currentEmployee.IsOnlyDentist() && appointment.DentistId != currentEmployee.EmployeeId)
            return Result.Forbidden(Messages.AppointmentNotAssigned);

        if (currentEmployee.IsNotInOffice(appointment.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        appointment.AppointmentStatusId = request.StatusId;
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
