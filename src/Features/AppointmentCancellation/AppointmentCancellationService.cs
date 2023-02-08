namespace DentallApp.Features.AppointmentCancellation;

public partial class AppointmentCancellationService
{
    private readonly IAppointmentCancellationRepository _appointmentRepository;
    private readonly IInstantMessaging _instantMessaging;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentCancellationService(IAppointmentCancellationRepository appointmentRepository,
                                          IInstantMessaging instantMessaging,
                                          IDateTimeProvider dateTimeProvider)
    {
        _appointmentRepository = appointmentRepository;
        _instantMessaging = instantMessaging;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response> CancelBasicUserAppointmentAsync(int appointmentId, int currentUserId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment is null)
            return new Response(ResourceNotFoundMessage);

        if (appointment.UserId != currentUserId)
            return new Response(AppointmentNotAssignedMessage);

        if (_dateTimeProvider.Now > (appointment.Date + appointment.StartHour))
            return new Response(AppointmentThatHasAlreadyPassedBasicUserMessage);

        appointment.AppointmentStatusId = AppointmentStatusId.Canceled;
        await _appointmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response<AppointmentsThatCannotBeCanceledDto>> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppointmentCancelDto appointmentCancelDto)
    {
        // Almacena las citas cuya fecha y hora estipulada NO hayan pasado.
        var appointmentsCanBeCancelled = appointmentCancelDto.Appointments
                                                       .Where(appointmentDto =>
                                                             (appointmentDto.AppointmentDate + appointmentDto.StartHour) > _dateTimeProvider.Now);
        var appointmentsIdCanBeCancelled = appointmentsCanBeCancelled.Select(appointmentDto => appointmentDto.AppointmentId);
        try
        {
            if (currentEmployee.IsOnlyDentist())
                await _appointmentRepository.CancelAppointmentsByDentistIdAsync(currentEmployee.GetEmployeeId(), appointmentsIdCanBeCancelled);
            else
                await _appointmentRepository.CancelAppointmentsByOfficeIdAsync(
                        currentEmployee.IsSuperAdmin() ? default : currentEmployee.GetOfficeId(),
                        appointmentsIdCanBeCancelled
                    );
            await SendMessageAboutAppointmentCancellationAsync(appointmentsCanBeCancelled, appointmentCancelDto.Reason);
        }
        catch (Exception ex)
        {
            return new Response<AppointmentsThatCannotBeCanceledDto>(ex.Message);
        }

        if (appointmentCancelDto.Appointments.Count() != appointmentsCanBeCancelled.Count())
        {
            int count = appointmentCancelDto.Appointments.Count() - appointmentsCanBeCancelled.Count();
            var message = string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, count);
            var data = new AppointmentsThatCannotBeCanceledDto
            {
                AppointmentsId = appointmentCancelDto.Appointments
                                                   .Select(appointmentDto => appointmentDto.AppointmentId)
                                                   .Except(appointmentsIdCanBeCancelled)
            };
            return new Response<AppointmentsThatCannotBeCanceledDto> { Message = message, Data = data };
        }

        return new Response<AppointmentsThatCannotBeCanceledDto>
        {
            Success = true,
            Message = SuccessfullyCancelledAppointmentsMessage
        };
    }
}
