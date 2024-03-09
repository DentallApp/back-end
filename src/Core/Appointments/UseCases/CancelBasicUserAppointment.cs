namespace DentallApp.Core.Appointments.UseCases;

public class CancelBasicUserAppointmentUseCase(
    IUnitOfWork unitOfWork,
    IRepository<Appointment> repository,
    IDateTimeService dateTimeService,
    ICurrentUser currentUser)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        var appointment = await repository.GetByIdAsync(id);

        if (appointment is null)
            return Result.NotFound();

        if (appointment.UserId != currentUser.UserId)
            return Result.Forbidden(Messages.AppointmentNotAssigned);

        if (dateTimeService.Now > (appointment.Date + appointment.StartHour))
            return Result.Forbidden(Messages.AppointmentThatHasAlreadyPassedBasicUser);

        appointment.AppointmentStatusId = (int)AppointmentStatus.Predefined.Canceled;
        await unitOfWork.SaveChangesAsync();

        return Result.DeletedResource();
    }
}
