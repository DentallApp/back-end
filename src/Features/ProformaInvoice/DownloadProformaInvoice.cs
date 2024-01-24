namespace DentallApp.Features.ProformaInvoice;

public class DownloadProformaInvoiceRequest
{
    public class DentalTreatment
    {
        public string GeneralTreatmentName { get; init; }
        public string SpecificTreatmentName { get; init; }
        public double Price { get; init; }
    }

    public string FullName { get; init; }
    public string Document { get; init; }
    public DateTime DateIssue { get; init; }
    public double TotalPrice { get; init; }
    public IEnumerable<DentalTreatment> DentalTreatments { get; init; }

    public object MapToObject() => new
    {
        FullName,
        Document,
        DateIssue,
        TotalPrice,
        DentalTreatments
    };
}

public class DownloadProformaInvoiceUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter)
{
    public async Task<byte[]> DownloadAsPdfAsync(DownloadProformaInvoiceRequest request)
    {
        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ProformaInvoice.html", request.MapToObject());
        return htmlConverter.ConvertToPdf(html, new MemoryStream());
    }
}
