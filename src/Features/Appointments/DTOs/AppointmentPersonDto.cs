namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentPersonDto : AppointmentGetDto
{
    public string Document { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
    public DateTime? DateBirth { get; set; }
}
