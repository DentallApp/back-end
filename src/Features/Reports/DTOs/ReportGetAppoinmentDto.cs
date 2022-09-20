namespace DentallApp.Features.Reports.DTOs;

public class ReportGetAppoinmentDto
{
    public string AppoinmentDate { get; set; }
    public string StartHour { get; set; }
    public string PatientName { get; set; }
    public string DentalServiceName { get; set; }
    public string DentistName { get; set; }
    public string OfficeName { get; set; }
    public string AppoinmentStatus { get; set; }
}
