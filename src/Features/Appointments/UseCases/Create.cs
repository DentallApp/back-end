﻿namespace DentallApp.Features.Appointments.UseCases;

public class CreateAppointmentUseCase : ICreateAppointmentUseCase
{
    private readonly DbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly SendAppointmentInformationUseCase _sendInformationUseCase;

    public CreateAppointmentUseCase(
        DbContext context, 
        IDateTimeService dateTimeService, 
        SendAppointmentInformationUseCase sendInformationUseCase)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _sendInformationUseCase = sendInformationUseCase;
    }

    public async Task<Result<CreatedId>> ExecuteAsync(CreateAppointmentRequest request)
    {
        // Checks if the date and time of the appointment is not available.
        bool isNotAvailable = await _context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == request.DentistId) &&
                  (appointment.Date == request.AppointmentDate) &&
                  (appointment.StartHour == request.StartHour) &&
                  (appointment.EndHour == request.EndHour) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   _dateTimeService.Now > DBFunctions.AddTime(DBFunctions.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => true)
            .AnyAsync();

        if (isNotAvailable)
            return Result.Failure(DateAndTimeAppointmentIsNotAvailableMessage);

        var appointment = request.MapToAppointment();
        _context.Add(appointment);
        await _context.SaveChangesAsync();
        await _sendInformationUseCase.ExecuteAsync(appointment.Id, request);
        return Result.CreatedResource(appointment.Id);
    }
}
