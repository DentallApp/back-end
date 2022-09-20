namespace DentallApp.Features.Reports.DTOs;

public class ReportPostAppoinmentDownloadDto
{
    public string From { get; set; }
    public string To { get; set; }
    public IEnumerable<ReportPostAppoinmentDownloadDetailsDto> Appoinments { get; set; }
}
