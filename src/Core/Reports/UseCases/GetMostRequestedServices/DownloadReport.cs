namespace DentallApp.Core.Reports.UseCases.GetMostRequestedServices;

public class DownloadDentalServicesReportRequest
{
    public class Service
    {
        public string DentalServiceName { get; init; }
        public int TotalAppointmentsAssisted { get; init; }
    }

    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public string OfficeName { get; init; }
    public IEnumerable<Service> Services { get; init; }

    public object MapToObject() => new
    {
        From = From.GetDateWithStandardFormat(),
        To   = To.GetDateWithStandardFormat(),
        OfficeName,
        Services
    };
}

public class DownloadDentalServicesReportValidator 
    : AbstractValidator<DownloadDentalServicesReportRequest>
{
    public DownloadDentalServicesReportValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.OfficeName).NotEmpty();
        RuleFor(request => request.Services).NotEmpty();
        RuleForEach(request => request.Services)
            .ChildRules(validator =>
            {
                validator
                    .RuleFor(service => service.DentalServiceName)
                    .NotEmpty();

                validator
                    .RuleFor(service => service.TotalAppointmentsAssisted)
                    .GreaterThanOrEqualTo(0);
            });
    }
}

public class DownloadDentalServicesReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter,
    DownloadDentalServicesReportValidator validator)
{
    public async Task<Result<ByteArrayFileContent>> DownloadAsPdfAsync(
        DownloadDentalServicesReportRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportDentalServices.html", request.MapToObject());

        byte[] fileContents = htmlConverter.ConvertToPdf(html, new MemoryStream());
        return Result.File(new ByteArrayFileContent(fileContents)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName = "DentalServices.pdf"
        });
    }
}
