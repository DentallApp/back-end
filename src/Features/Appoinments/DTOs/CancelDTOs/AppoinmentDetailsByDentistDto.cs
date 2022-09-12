namespace DentallApp.Features.Appoinments.DTOs.CancelDTOs;

public class AppoinmentDetailsByDentistDto
{
    public int AppoinmentId { get; set; }
    public string PatientCellPhone { get; set; }
    public DateTime AppoinmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
