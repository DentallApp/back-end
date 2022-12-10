namespace DentallApp.Features.AppointmentCancellation.DTOs;

public class AppointmentCancelDetailsDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; }
    public string PatientCellPhone { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
}
