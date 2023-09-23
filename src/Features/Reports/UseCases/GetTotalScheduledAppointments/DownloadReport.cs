namespace DentallApp.Features.Reports.UseCases.GetTotalScheduledAppointments;

public class DownloadScheduledAppointmentsReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public IEnumerable<GetTotalScheduledAppointmentsResponse> Appointments { get; init; }

    public object MapToObject()
    {
        return new
        {
            From,
            To,
            Appointments
        };
    }
}

public class DownloadScheduledAppointmentsReportUseCase
{
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public DownloadScheduledAppointmentsReportUseCase(
        IHtmlTemplateLoader htmlTemplateLoader,
        IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> DownloadAsPdfAsync(DownloadScheduledAppointmentsReportRequest request)
    {
        var html = await _htmlTemplateLoader
            .LoadAsync("./Templates/ReportScheduledAppointment.html", request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
