namespace DentallApp.Features.Appointments.UseCases;

public class CancelBasicUserAppointmentUseCase(
    IUnitOfWork unitOfWork,
    IRepository<Appointment> repository,
    IDateTimeService dateTimeService)
{
    public async Task<Result> ExecuteAsync(int appointmentId, int currentUserId)
    {
        var appointment = await repository.GetByIdAsync(appointmentId);

        if (appointment is null)
            return Result.NotFound();

        if (appointment.UserId != currentUserId)
            return Result.Forbidden(Messages.AppointmentNotAssigned);

        if (dateTimeService.Now > (appointment.Date + appointment.StartHour))
            return Result.Forbidden(Messages.AppointmentThatHasAlreadyPassedBasicUser);

        appointment.AppointmentStatusId = (int)AppointmentStatus.Predefined.Canceled;
        await unitOfWork.SaveChangesAsync();

        return Result.DeletedResource();
    }
}
