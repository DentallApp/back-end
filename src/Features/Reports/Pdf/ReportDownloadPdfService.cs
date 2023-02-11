namespace DentallApp.Features.Reports.Pdf;

public class ReportDownloadPdfService
{
    private const string Path = "./Templates/{0}.html";
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public ReportDownloadPdfService(IHtmlTemplateLoader htmlTemplateLoader, IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> CreateReportTotalAppointmentPdfAsync(ReportTotalAppointmentDownloadRequest request)
    {
        var html = await _htmlTemplateLoader.LoadAsync(string.Format(Path, "ReportAppointment"), request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }

    public async Task<byte[]> CreateReportTotalScheduledAppointmentPdfAsync(ReportTotalScheduledAppointmentDownloadRequest request)
    {
        var html = await _htmlTemplateLoader.LoadAsync(string.Format(Path, "ReportScheduledAppointment"), request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }

    public async Task<byte[]> CreateReportDentalServiceDownloadPdfAsync(ReportDentalServicesDownloadRequest request)
    {
        var html = await _htmlTemplateLoader.LoadAsync(string.Format(Path, "ReportDentalServices"), request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
