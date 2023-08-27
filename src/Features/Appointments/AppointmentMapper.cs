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
}
