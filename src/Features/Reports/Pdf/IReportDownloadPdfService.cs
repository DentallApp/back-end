namespace DentallApp.Features.Reports.Pdf;

public interface IReportDownloadPdfService
{
    Task<byte[]> CreateReportAppoinmentPdfAsync(ReportPostAppoinmentDownloadDto reportPostDownloadDto);
    Task<byte[]> CreateReportScheduledAppoinmentPdfAsync(ReportPostScheduledDownloadDto reportPostDownloadDto);
}
