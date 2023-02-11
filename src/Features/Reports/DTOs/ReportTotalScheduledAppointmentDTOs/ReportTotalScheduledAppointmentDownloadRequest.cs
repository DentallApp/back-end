namespace DentallApp.Features.Reports.DTOs;

public class ReportTotalScheduledAppointmentDownloadRequest
{
    public string From { get; set; }
    public string To { get; set; }
    public IEnumerable<ReportGetTotalScheduledAppointmentResponse> Appointments { get; set; }
}
