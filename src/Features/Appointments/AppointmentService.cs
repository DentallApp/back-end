namespace DentallApp.Features.Appointments;

public partial class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IInstantMessaging _instantMessaging;
    private readonly ISpecificTreatmentRepository _treatmentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentService(IAppointmentRepository appointmentRepository, 
                             IInstantMessaging instantMessaging,
                             ISpecificTreatmentRepository treatmentRepository,
                             IDateTimeProvider dateTimeProvider)
    {
        _appointmentRepository = appointmentRepository;
        _instantMessaging = instantMessaging;
        _treatmentRepository = treatmentRepository;
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

    public async Task<Response> CreateAppointmentAsync(AppointmentInsertDto appointmentInsertDto)
    {
        if (await _appointmentRepository.IsNotAvailableAsync(appointmentInsertDto))
            return new Response(DateAndTimeAppointmentIsNotAvailableMessage);

        var appointment = appointmentInsertDto.MapToAppointment();
        _appointmentRepository.Insert(appointment);
        await _appointmentRepository.SaveAsync();
        await SendAppoinmentInformationAsync(appointment.Id, appointmentInsertDto);
        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateAppointmentAsync(int id, ClaimsPrincipal currentEmployee, AppointmentUpdateDto appointmentUpdateDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment is null)
            return new Response(ResourceNotFoundMessage);

        if (appointment.AppointmentStatusId == AppointmentStatusId.Canceled)
            return new Response(AppointmentIsAlreadyCanceledMessage);

        if (_dateTimeProvider.Now.Date > appointment.Date)
            return new Response(AppointmentCannotBeUpdatedForPreviousDaysMessage);

        if (currentEmployee.IsOnlyDentist() && appointment.DentistId != currentEmployee.GetEmployeeId())
            return new Response(AppointmentNotAssignedMessage);

        if (currentEmployee.IsNotInOffice(appointment.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        appointmentUpdateDto.MapToAppointment(appointment);
        await _appointmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
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
            await SendMessageAboutAppoinmentCancellationAsync(appointmentsCanBeCancelled, appointmentCancelDto.Reason);
        }
        catch(Exception ex)
        {
            return new Response<AppointmentsThatCannotBeCanceledDto>(ex.Message);
        }

        if(appointmentCancelDto.Appointments.Count() != appointmentsCanBeCancelled.Count())
        {
            int count   = appointmentCancelDto.Appointments.Count() - appointmentsCanBeCancelled.Count();
            var message = string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, count);
            var data    = new AppointmentsThatCannotBeCanceledDto
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

    public async Task<Response<IEnumerable<AppointmentGetByEmployeeDto>>> GetAppointmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppointmentPostDateDto appointmentPostDto)
    {
        if (currentEmployee.IsOnlyDentist() && currentEmployee.GetEmployeeId() != appointmentPostDto.DentistId)
            return new Response<IEnumerable<AppointmentGetByEmployeeDto>>(CanOnlyAccessYourOwnAppointmentsMessage);

        if (!currentEmployee.IsSuperAdmin() && currentEmployee.IsNotInOffice(appointmentPostDto.OfficeId))
            return new Response<IEnumerable<AppointmentGetByEmployeeDto>>(OfficeNotAssignedMessage);

        var data = await _appointmentRepository.GetAppointmentsForEmployeeAsync(appointmentPostDto);
        return new Response<IEnumerable<AppointmentGetByEmployeeDto>>
        {
            Success = true,
            Message = GetResourceMessage,
            Data = data
        };
    }
}
