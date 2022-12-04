namespace DentallApp.Features.AppointmentsStatus;

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
