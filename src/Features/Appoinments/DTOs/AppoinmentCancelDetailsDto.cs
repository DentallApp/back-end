namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentCancelDetailsDto
{
    public int AppoinmentId { get; set; }
    public string PatientName { get; set; }
    public string PatientCellPhone { get; set; }
    public DateTime AppoinmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
}
