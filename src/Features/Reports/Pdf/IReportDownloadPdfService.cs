namespace DentallApp.Features.Reports.Pdf;

public interface IReportDownloadPdfService
{
    Task<byte[]> CreateReportTotalAppointmentPdfAsync(ReportPostTotalAppointmentDownloadDto reportPostDownloadDto);
    Task<byte[]> CreateReportTotalScheduledAppointmentPdfAsync(ReportPostScheduledDownloadDto reportPostDownloadDto);
    Task<byte[]> CreateReportDentalServiceDownloadPdfAsync(ReportPostDentalServiceDto reportPostDto);
}
