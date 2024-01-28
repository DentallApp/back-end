namespace DentallApp.Shared.Reasons;

public readonly ref struct ScheduledAppointmentSuccess
{
    public string Message { get; }
    public ScheduledAppointmentSuccess(string rangeToPay)
        => Message = string.Format(Messages.SuccessfullyScheduledAppointment, rangeToPay ?? string.Empty);
}
