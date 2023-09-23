namespace DentallApp.Features.Reports.UseCases.GetMostRequestedServices;

public class DownloadDentalServicesReportRequest
{
    public string From { get; init; }
    public string To { get; init; }
    public string OfficeName { get; init; }
    public IEnumerable<GetMostRequestedServicesResponse> Services { get; init; }

    public object MapToObject()
    {
        return new
        {
            From,
            To,
            OfficeName,
            Services
        };
    }
}

public class DownloadDentalServicesReportUseCase
{
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public DownloadDentalServicesReportUseCase(
        IHtmlTemplateLoader htmlTemplateLoader,
        IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> DownloadAsPdfAsync(DownloadDentalServicesReportRequest request)
    {
        var html = await _htmlTemplateLoader
            .LoadAsync("./Templates/ReportDentalServices.html", request.MapToObject());
        return _htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
