namespace DentallApp.Features.Reports.DTOs;

public class ReportGetTotalAppoinmentDto
{
    public int Total { get; set; }
    public int TotalAppoinmentsAssisted { get; set; }
    public int TotalAppoinmentsNotAssisted { get; set; }
    public int TotalAppoinmentsCancelledByPatient { get; set; }
    public int TotalAppoinmentsCancelledByEmployee { get; set; }
}
