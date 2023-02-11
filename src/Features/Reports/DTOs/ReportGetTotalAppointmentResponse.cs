namespace DentallApp.Features.Reports.DTOs;

public class ReportGetTotalAppointmentResponse
{
    public int Total { get; set; }
    public int TotalAppointmentsAssisted { get; set; }
    public int TotalAppointmentsNotAssisted { get; set; }
    public int TotalAppointmentsCancelledByPatient { get; set; }
    public int TotalAppointmentsCancelledByEmployee { get; set; }
}
