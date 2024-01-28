namespace DentallApp.Shared.Reasons;

public readonly ref struct SuccessfullyScheduledAppointmentSuccess
{
    public string Message { get; }
    public SuccessfullyScheduledAppointmentSuccess(string rangeToPay)
        => Message = string.Format(Messages.SuccessfullyScheduledAppointment, rangeToPay ?? string.Empty);
}
