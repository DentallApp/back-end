namespace DentallApp.Features.Reports.UseCases.GetTotalAppointments;

public class DownloadTotalAppointmentsReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public string OfficeName { get; init; }
    public string DentistName { get; init; }
    public GetTotalAppointmentsResponse Totals { get; init; }

    public object MapToObject()
    {
        return new
        {
            From,
            To,
            OfficeName,
            DentistName,
            Totals
        };
    }
}

public class DownloadTotalAppointmentsReportUseCase
{
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public DownloadTotalAppointmentsReportUseCase(
        IHtmlTemplateLoader htmlTemplateLoader, 
        IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> DownloadAsPdfAsync(DownloadTotalAppointmentsReportRequest request)
    {
        var html = await _htmlTemplateLoader
            .LoadAsync("./Templates/ReportAppointment.html", request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
