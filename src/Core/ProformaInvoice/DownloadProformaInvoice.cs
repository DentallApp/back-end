namespace DentallApp.Core.ProformaInvoice;

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

public class DownloadProformaInvoiceValidator 
    : AbstractValidator<DownloadProformaInvoiceRequest>
{
    public DownloadProformaInvoiceValidator()
    {
        RuleFor(request => request.FullName).NotEmpty();
        RuleFor(request => request.Document).NotEmpty();
        RuleFor(request => request.TotalPrice).GreaterThan(0);
        RuleFor(request => request.DentalTreatments).NotEmpty();
        RuleForEach(request => request.DentalTreatments)
            .ChildRules(validator =>
            {
                validator
                    .RuleFor(treatment => treatment.GeneralTreatmentName)
                    .NotEmpty();

                validator
                    .RuleFor(treatment => treatment.SpecificTreatmentName)
                    .NotEmpty();

                validator
                    .RuleFor(treatment => treatment.Price)
                    .GreaterThan(0);
            });
    }
}

public class DownloadProformaInvoiceUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter,
    DownloadProformaInvoiceValidator validator)
{
    public async Task<Result<ByteArrayFileContent>> DownloadAsPdfAsync(DownloadProformaInvoiceRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ProformaInvoice.html", request.MapToObject());

        byte[] fileContents = htmlConverter.ConvertToPdf(html, new MemoryStream());
        return Result.File(new ByteArrayFileContent(fileContents)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName = "ProformaInvoice.pdf"
        });
    }
}
