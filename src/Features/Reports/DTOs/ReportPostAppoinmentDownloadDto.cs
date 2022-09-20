namespace DentallApp.Features.Reports.DTOs;

public class ReportPostAppoinmentDownloadDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public IEnumerable<ReportPostAppoinmentDownloadDetailsDto> Appoinments { get; set; }
}
