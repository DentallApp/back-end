namespace DentallApp.Features.Reports.DTOs;

public class ReportGetTotalScheduledAppointmentResponse
{
    public string DentistName { get; set; }
    public string OfficeName { get; set; }
    public int Total { get; set; }
}
