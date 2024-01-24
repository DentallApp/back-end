namespace DentallApp.Features.Reports.UseCases.GetMostRequestedServices;

public class DownloadDentalServicesReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public string OfficeName { get; init; }
    public IEnumerable<GetMostRequestedServicesResponse> Services { get; init; }

    public object MapToObject() => new
    {
        From,
        To,
        OfficeName,
        Services
    };
}

public class DownloadDentalServicesReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter)
{
    public async Task<byte[]> DownloadAsPdfAsync(DownloadDentalServicesReportRequest request)
    {
        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportDentalServices.html", request.MapToObject());
        return htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
