namespace DentallApp.Features.Appointments.UseCases;

public class CancelBasicUserAppointmentUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Appointment> _repository;
    private readonly IDateTimeService _dateTimeService;

    public CancelBasicUserAppointmentUseCase(
        IUnitOfWork unitOfWork,
        IRepository<Appointment> repository, 
        IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result> ExecuteAsync(int appointmentId, int currentUserId)
    {
        var appointment = await _repository.GetByIdAsync(appointmentId);

        if (appointment is null)
            return Result.NotFound();

        if (appointment.UserId != currentUserId)
            return Result.Forbidden(AppointmentNotAssignedMessage);

        if (_dateTimeService.Now > (appointment.Date + appointment.StartHour))
            return Result.Forbidden(AppointmentThatHasAlreadyPassedBasicUserMessage);

        appointment.AppointmentStatusId = AppointmentStatusId.Canceled;
        await _unitOfWork.SaveChangesAsync();

        return Result.DeletedResource();
    }
}
