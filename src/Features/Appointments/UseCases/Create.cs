namespace DentallApp.Features.Appointments.UseCases;

public class CreateAppointmentUseCase(
    DbContext context,
    IDateTimeService dateTimeService,
    SendAppointmentInformationUseCase sendInformationUseCase) : ICreateAppointmentUseCase
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateAppointmentRequest request)
    {
        // Checks if the date and time of the appointment is not available.
        bool isNotAvailable = await context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == request.DentistId) &&
                  (appointment.Date == request.AppointmentDate) &&
                  (appointment.StartHour == request.StartHour) &&
                  (appointment.EndHour == request.EndHour) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   dateTimeService.Now > DBFunctions.AddTime(DBFunctions.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => true)
            .AnyAsync();

        if (isNotAvailable)
            return Result.Failure(Messages.DateAndTimeAppointmentIsNotAvailable);

        var appointment = request.MapToAppointment();
        context.Add(appointment);
        await context.SaveChangesAsync();
        await sendInformationUseCase.ExecuteAsync(appointment.Id, request);
        return Result.CreatedResource(appointment.Id);
    }
}
