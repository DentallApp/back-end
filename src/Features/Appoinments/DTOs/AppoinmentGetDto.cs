namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentGetDto
{
    public int AppoinmentId { get; set; }
    public string PatientName { get; set; }
    public string CreatedAt { get; set; }
    public string AppointmentDate { get; set; }
    public string StartHour { get; set; }
    public string EndHour { get; set; }
    public string DentalServiceName { get; set; }
}
