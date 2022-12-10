namespace DentallApp.Features.AppointmentCancellation.DTOs;

public class AppointmentCancelDto
{
    public string Reason { get; set; }
    public IEnumerable<AppointmentCancelDetailsDto> Appointments { get; set; }
}
