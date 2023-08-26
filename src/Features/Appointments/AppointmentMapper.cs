namespace DentallApp.Features.Appointments;

public static class AppointmentMapper
{
    [Decompile]
    public static UnavailableTimeRangeDto MapToUnavailableTimeRangeDto(this Appointment appointment)
        => new()
        {
            StartHour  = appointment.StartHour,
            EndHour    = appointment.EndHour
        };

    [Decompile]
    public static AppointmentInfoDto MapToAppointmentInfoDto(this Appointment appointment)
        => new()
        {
            PatientName         = appointment.Person.FullName,
            CellPhone           = appointment.Person.CellPhone,
            DentistName         = appointment.Employee.Person.FullName,
            DentalServiceName   = appointment.GeneralTreatment.Name,
            OfficeName          = appointment.Office.Name
        };
}
