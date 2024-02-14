namespace DentallApp.Core.Reports.UseCases.GetTotalScheduledAppointments;

public class DownloadScheduledAppointmentsReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public IEnumerable<GetTotalScheduledAppointmentsResponse> Appointments { get; init; }

    public object MapToObject() => new
    {
        From,
        To,
        Appointments
    };
}

public class DownloadScheduledAppointmentsReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter)
{
    public async Task<byte[]> DownloadAsPdfAsync(DownloadScheduledAppointmentsReportRequest request)
    {
        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportScheduledAppointment.html", request.MapToObject());
        return htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
