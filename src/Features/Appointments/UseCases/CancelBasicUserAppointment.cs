﻿namespace DentallApp.Features.Appointments.UseCases;

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

    public async Task<Response> Execute(int appointmentId, int currentUserId)
    {
        var appointment = await _repository.GetByIdAsync(appointmentId);

        if (appointment is null)
            return new Response(ResourceNotFoundMessage);

        if (appointment.UserId != currentUserId)
            return new Response(AppointmentNotAssignedMessage);

        if (_dateTimeService.Now > (appointment.Date + appointment.StartHour))
            return new Response(AppointmentThatHasAlreadyPassedBasicUserMessage);

        appointment.AppointmentStatusId = AppointmentStatusId.Canceled;
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
