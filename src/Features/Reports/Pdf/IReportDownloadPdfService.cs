namespace DentallApp.Features.Reports.Pdf;

public interface IReportDownloadPdfService
{
    Task<byte[]> CreateReportTotalAppoinmentPdfAsync(ReportPostTotalAppoinmentDownloadDto reportPostDownloadDto);
    Task<byte[]> CreateReportTotalScheduledAppoinmentPdfAsync(ReportPostScheduledDownloadDto reportPostDownloadDto);
    Task<byte[]> CreateReportDentalServiceDownloadPdfAsync(ReportPostDentalServiceDto reportPostDto);
}
