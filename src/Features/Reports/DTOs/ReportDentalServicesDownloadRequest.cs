namespace DentallApp.Features.Reports.DTOs;

public class ReportDentalServicesDownloadRequest
{
    public string From { get; set; }
    public string To { get; set; }
    public string OfficeName { get; set; }
    public IEnumerable<ReportGetMostRequestedServiceResponse> Services { get; set; }
}
