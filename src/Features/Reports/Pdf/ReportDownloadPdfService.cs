namespace DentallApp.Features.Reports.Pdf;

public class ReportDownloadPdfService : IReportDownloadPdfService
{
    private const string Path = "./Templates/{0}.html";
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public ReportDownloadPdfService(IHtmlTemplateLoader htmlTemplateLoader, IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> CreateReportAppoinmentPdfAsync(ReportPostAppoinmentDownloadDto reportPostDownloadDto)
    {
        var html = await _htmlTemplateLoader.LoadAsync(string.Format(Path, "ReportAppoinment"), reportPostDownloadDto.MapToObject());
        var pdfStream = new MemoryStream();
        _htmlConverter.ConvertToPdf(html, pdfStream);
        return pdfStream.ToArray();
    }
}
