namespace DentallApp.Shared.Reasons;

public readonly ref struct AppointmentThatHasAlreadyPassedEmployeeError
{
    public string Message { get; }
    public AppointmentThatHasAlreadyPassedEmployeeError(int pastAppointments)
        => Message = string.Format(Messages.AppointmentThatHasAlreadyPassedEmployee, pastAppointments);
}
