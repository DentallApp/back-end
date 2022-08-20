namespace DentallApp.Features.ProformaInvoice;

public class ProformaInvoiceService : IProformaInvoiceService
{
    private readonly IHtmlTemplateLoader _htmlTemplateLoader;
    private readonly IHtmlConverter _htmlConverter;

    public ProformaInvoiceService(IHtmlTemplateLoader htmlTemplateLoader, IHtmlConverter htmlConverter)
    {
        _htmlTemplateLoader = htmlTemplateLoader;
        _htmlConverter = htmlConverter;
    }

    public async Task<byte[]> CreateProformaInvoicePdfAsync(ProformaInvoiceDto proformaInvoiceDto)
    {
        var html = await _htmlTemplateLoader.LoadAsync("./Templates/ProformaInvoice.html", proformaInvoiceDto.MapToObject());
        var pdfStream = new MemoryStream();
        _htmlConverter.ConvertToPdf(html, pdfStream);
        return pdfStream.ToArray();
    }
}
