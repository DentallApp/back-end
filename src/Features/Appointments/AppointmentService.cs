namespace DentallApp.Features.Appointments;

public class AppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentService(IAppointmentRepository appointmentRepository,
                              IDateTimeProvider dateTimeProvider)
    {
        _appointmentRepository = appointmentRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response<InsertedIdDto>> CreateAppointmentAsync(AppointmentInsertDto appointmentInsertDto)
    {
        if (await _appointmentRepository.IsNotAvailableAsync(appointmentInsertDto))
            return new Response<InsertedIdDto>(DateAndTimeAppointmentIsNotAvailableMessage);

        var appointment = appointmentInsertDto.MapToAppointment();
        _appointmentRepository.Insert(appointment);
        await _appointmentRepository.SaveAsync();
        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = appointment.Id },
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
