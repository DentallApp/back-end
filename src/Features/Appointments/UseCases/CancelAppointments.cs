using LinqToDB;

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
    private readonly AppDbContext _context;
    private readonly IInstantMessaging _instantMessaging;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CancelAppointmentsUseCase(
        AppDbContext context,
        IInstantMessaging instantMessaging,
        IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _instantMessaging = instantMessaging;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response<CancelAppointmentsResponse>> Execute(ClaimsPrincipal currentEmployee, CancelAppointmentsRequest request)
    {
        // Stores appointments whose stipulated date and time have not passed.
        var appointmentsCanBeCancelled = request.Appointments
            .Where(appointmentDto => (appointmentDto.AppointmentDate + appointmentDto.StartHour) > _dateTimeProvider.Now);

        var appointmentsIdCanBeCancelled = appointmentsCanBeCancelled
            .Select(appointmentDto => appointmentDto.AppointmentId);

        if (currentEmployee.IsOnlyDentist())
        {
            await CancelAppointmentsByDentistId(currentEmployee.GetEmployeeId(), appointmentsIdCanBeCancelled);
        }
        else
        {
            int officeId = currentEmployee.IsSuperAdmin() ? default : currentEmployee.GetOfficeId();
            await CancelAppointmentsByOfficeId(officeId, appointmentsIdCanBeCancelled);
        }

        await SendMessageAboutAppointmentCancellation(appointmentsCanBeCancelled, request.Reason);

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
            return new Response<CancelAppointmentsResponse>
            {
                Message = message,
                Data    = appointmentsThatCannotBeCanceled
            };
        }

        return new Response<CancelAppointmentsResponse>
        {
            Success = true,
            Message = SuccessfullyCancelledAppointmentsMessage
        };
    }

    private async Task SendMessageAboutAppointmentCancellation(
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

    /// <summary>
    /// Cancela las citas por parte del empleado.
    /// </summary>
    private async Task<int> CancelAppointments(int officeId, int dentistId, IEnumerable<int> appointmentsId)
    {
        if (appointmentsId.Any())
        {
            int updatedRows = await _context.Set<Appointment>()
                .OptionalWhere(officeId, appointment => appointment.OfficeId == officeId)
                .OptionalWhere(dentistId, appointment => appointment.DentistId == dentistId)
                .Where(appointment =>
                       appointment.AppointmentStatusId == AppointmentStatusId.Scheduled &&
                       appointmentsId.Contains(appointment.Id))
                .Set(appointment => appointment.AppointmentStatusId, AppointmentStatusId.Canceled)
                .Set(appointment => appointment.IsCancelledByEmployee, true)
                .Set(appointment => appointment.UpdatedAt, _dateTimeProvider.Now)
                .UpdateAsync();

            return updatedRows;
        }

        return default;
    }

    private Task<int> CancelAppointmentsByOfficeId(int officeId, IEnumerable<int> appointmentsId)
    {
        return CancelAppointments(officeId, dentistId: default, appointmentsId);
    }

    private Task<int> CancelAppointmentsByDentistId(int dentistId, IEnumerable<int> appointmentsId)
    {
        return CancelAppointments(officeId: default, dentistId, appointmentsId);
    }
}
