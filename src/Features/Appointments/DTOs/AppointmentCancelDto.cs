namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentCancelDto
{
    public string Reason { get; set; }
    public IEnumerable<AppointmentCancelDetailsDto> Appointments { get; set; }
}

public class AppointmentsThatCannotBeCanceledDto
{
    public IEnumerable<int> AppointmentsId { get; set; }
}
