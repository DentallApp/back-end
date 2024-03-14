namespace DentallApp.Core.Appointments.UseCases;

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

public class CancelAppointmentsValidator : AbstractValidator<CancelAppointmentsRequest>
{
    public CancelAppointmentsValidator()
    {
        RuleFor(request => request.Reason).NotEmpty();
        RuleFor(request => request.Appointments).NotEmpty();
        RuleForEach(request => request.Appointments)
            .ChildRules(validator =>
            {
                validator
                    .RuleFor(appointment => appointment.AppointmentId)
                    .GreaterThan(0);

                validator
                    .RuleFor(appointment => appointment.PatientName)
                    .NotEmpty();

                validator
                    .RuleFor(appointment => appointment.PatientCellPhone)
                    .NotEmpty();
            });
    }
}

public class AppointmentsThatCannotBeCanceledResponse
{
    public IEnumerable<int> AppointmentsId { get; init; }
}

public class CancelAppointmentsUseCase(
    AppSettings settings,
    IAppointmentRepository appointmentRepository,
    IInstantMessaging instantMessaging,
    IDateTimeService dateTimeService,
    ICurrentEmployee currentEmployee,
    CancelAppointmentsValidator validator)
{
    public async Task<Result<AppointmentsThatCannotBeCanceledResponse>> ExecuteAsync(CancelAppointmentsRequest request)
    {
        var result = validator.Validate(request);
        if(result.IsFailed()) 
            return result.Invalid();

        // Stores appointments whose stipulated date and time have not passed.
        var appointmentsCanBeCancelled = request.Appointments
            .Where(appointment => (appointment.AppointmentDate + appointment.StartHour) > dateTimeService.Now);

        var appointmentsIdCanBeCancelled = appointmentsCanBeCancelled
            .Select(appointment => appointment.AppointmentId);

        if (currentEmployee.IsOnlyDentist())
        {
            await appointmentRepository
                .CancelAppointmentsByDentistIdAsync(currentEmployee.EmployeeId, appointmentsIdCanBeCancelled);
        }
        else
        {
            int officeId = currentEmployee.IsSuperAdmin() ? default : currentEmployee.OfficeId;
            await appointmentRepository
                .CancelAppointmentsByOfficeIdAsync(officeId, appointmentsIdCanBeCancelled);
        }

        await SendMessageAboutAppointmentCancellationAsync(appointmentsCanBeCancelled, request.Reason);

        // Verify if there are appointments that cannot be cancelled.
        if (request.Appointments.Count() != appointmentsCanBeCancelled.Count())
        {
            int pastAppointments = request.Appointments.Count() - appointmentsCanBeCancelled.Count();
            var message = new AppointmentThatHasAlreadyPassedEmployeeError(pastAppointments).Message;
            var response = new AppointmentsThatCannotBeCanceledResponse
            {
                AppointmentsId = request.Appointments
                    .Select(appointment => appointment.AppointmentId)
                    .Except(appointmentsIdCanBeCancelled)
            };

            return Result
                .Invalid(message)
                .WithData(response);
        }

        return Result.Success(Messages.SuccessfullyCancelledAppointments);
    }

    private async Task SendMessageAboutAppointmentCancellationAsync(
        IEnumerable<CancelAppointmentsRequest.Appointment> appointmentsCanBeCancelled,
        string reason)
    {
        foreach (var appointment in appointmentsCanBeCancelled)
        {
            var msg = string.Format(Messages.AppointmentCancellation,
                appointment.PatientName,
                settings.BusinessName,
                appointment.AppointmentDate.GetDateInSpanishFormat(),
                appointment.StartHour.GetHourWithoutSeconds(),
                reason);
            await instantMessaging.SendMessageAsync(appointment.PatientCellPhone, msg);
        }
    }
}
