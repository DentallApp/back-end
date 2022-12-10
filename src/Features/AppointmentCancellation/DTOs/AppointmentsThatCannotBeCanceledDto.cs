namespace DentallApp.Features.AppointmentCancellation.DTOs;

public class AppointmentsThatCannotBeCanceledDto
{
    public IEnumerable<int> AppointmentsId { get; set; }
}
