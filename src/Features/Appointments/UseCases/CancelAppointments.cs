namespace DentallApp.Features.Appointments.UseCases;

public class CancelAppointmentsRequest
{
    public class Appointment
    {
        public int AppointmentId { get; init; }
        public string PatientName { get; init; }
        public string PatientCellPhone { get; init; }
        public DateTime AppointmentDate { get; init; }
        public TimeSpan StartHour { get; init; }
    }

    public string Reason { get; init; }
    public IEnumerable<Appointment> Appointments { get; init; }
}

/// <summary>
/// Represents the appointments that cannot be canceled.
/// </summary>
public class CancelAppointmentsResponse
{
    public IEnumerable<int> AppointmentsId { get; init; }
}

public class CancelAppointmentsUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IInstantMessaging _instantMessaging;
    private readonly IDateTimeService _dateTimeService;

    public CancelAppointmentsUseCase(
        IAppointmentRepository appointmentRepository,
        IInstantMessaging instantMessaging,
        IDateTimeService dateTimeService)
    {
        _appointmentRepository = appointmentRepository;
        _instantMessaging = instantMessaging;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result<CancelAppointmentsResponse>> ExecuteAsync(ClaimsPrincipal currentEmployee, CancelAppointmentsRequest request)
    {
        // Stores appointments whose stipulated date and time have not passed.
        var appointmentsCanBeCancelled = request.Appointments
            .Where(appointmentDto => (appointmentDto.AppointmentDate + appointmentDto.StartHour) > _dateTimeService.Now);

        var appointmentsIdCanBeCancelled = appointmentsCanBeCancelled
            .Select(appointmentDto => appointmentDto.AppointmentId);

        if (currentEmployee.IsOnlyDentist())
        {
            await _appointmentRepository
                .CancelAppointmentsByDentistIdAsync(currentEmployee.GetEmployeeId(), appointmentsIdCanBeCancelled);
        }
        else
        {
            int officeId = currentEmployee.IsSuperAdmin() ? default : currentEmployee.GetOfficeId();
            await _appointmentRepository
                .CancelAppointmentsByOfficeIdAsync(officeId, appointmentsIdCanBeCancelled);
        }

        await SendMessageAboutAppointmentCancellationAsync(appointmentsCanBeCancelled, request.Reason);

        // Verify if there are appointments that cannot be cancelled.
        if (request.Appointments.Count() != appointmentsCanBeCancelled.Count())
        {
            int count = request.Appointments.Count() - appointmentsCanBeCancelled.Count();
            var message = string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, count);
            var appointmentsThatCannotBeCanceled = new CancelAppointmentsResponse
            {
                AppointmentsId = request.Appointments
                    .Select(appointmentDto => appointmentDto.AppointmentId)
                    .Except(appointmentsIdCanBeCancelled)
            };
            return new Result<CancelAppointmentsResponse>
            {
                Message = message,
                Data = appointmentsThatCannotBeCanceled,
                Status = ResultStatus.Invalid
            };
        }

        return Result.Success(SuccessfullyCancelledAppointmentsMessage);
    }

    private async Task SendMessageAboutAppointmentCancellationAsync(
        IEnumerable<CancelAppointmentsRequest.Appointment> appointmentsCanBeCancelled,
        string reason)
    {
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        foreach (var appointment in appointmentsCanBeCancelled)
        {
            var msg = string.Format(AppointmentCancellationMessageTemplate,
                appointment.PatientName,
                businessName,
                appointment.AppointmentDate.GetDateInSpanishFormat(),
                appointment.StartHour.GetHourWithoutSeconds(),
                reason);
            await _instantMessaging.SendMessageAsync(appointment.PatientCellPhone, msg);
        }
    }
}
