namespace DentallApp.Features.Reports.UseCases.GetTotalAppointments;

public class DownloadTotalAppointmentsReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public string OfficeName { get; init; }
    public string DentistName { get; init; }
    public GetTotalAppointmentsResponse Totals { get; init; }

    public object MapToObject() => new
    {
        From,
        To,
        OfficeName,
        DentistName,
        Totals
    };
}

public class DownloadTotalAppointmentsReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter)
{
    public async Task<byte[]> DownloadAsPdfAsync(DownloadTotalAppointmentsReportRequest request)
    {
        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportAppointment.html", request.MapToObject());
        return htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
