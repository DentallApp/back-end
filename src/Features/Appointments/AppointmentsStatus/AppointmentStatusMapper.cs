namespace DentallApp.Features.Appointments.AppointmentsStatus;

public static class AppointmentStatusMapper
{
    [Decompile]
    public static AppointmentStatusGetDto MapToAppointmentStatusGetDto(this AppointmentStatus appointmentStatus)
        => new()
        {
            Id = appointmentStatus.Id,
            Name = appointmentStatus.Name
        };
}
