namespace DentallApp.Shared.Reasons;

public class AppointmentThatHasAlreadyPassedEmployeeError
{
    public string Message { get; }
    public AppointmentThatHasAlreadyPassedEmployeeError(int pastAppointments)
        => Message = string.Format(Messages.AppointmentThatHasAlreadyPassedEmployee, pastAppointments);
}
