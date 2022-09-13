namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentCancelDetailsDto
{
    public int AppoinmentId { get; set; }
    public string PatientName { get; set; }
    public string PatientCellPhone { get; set; }
    public string AppoinmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
}
