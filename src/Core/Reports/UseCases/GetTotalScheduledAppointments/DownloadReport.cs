namespace DentallApp.Core.Reports.UseCases.GetTotalScheduledAppointments;

public class DownloadScheduledAppointmentsReportRequest
{
    public class TotalScheduledAppointments
    {
        public string DentistName { get; init; }
        public string OfficeName { get; init; }
        public int Total { get; init; }
    }

    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public IEnumerable<TotalScheduledAppointments> Appointments { get; init; }

    public object MapToObject() => new
    {
        From = From.GetDateWithStandardFormat(),
        To   = To.GetDateWithStandardFormat(),
        Appointments
    };
}

public class DownloadScheduledAppointmentsReportValidator 
    : AbstractValidator<DownloadScheduledAppointmentsReportRequest>
{
    public DownloadScheduledAppointmentsReportValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.Appointments).NotEmpty();
        RuleForEach(request => request.Appointments)
            .ChildRules(validator =>
            {
                validator 
                    .RuleFor(appointment => appointment.DentistName)
                    .NotEmpty();

                validator
                    .RuleFor(appointment => appointment.OfficeName)
                    .NotEmpty();

                validator
                    .RuleFor(appointment => appointment.Total)
                    .GreaterThanOrEqualTo(0);
            });
    }
}

public class DownloadScheduledAppointmentsReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter,
    DownloadScheduledAppointmentsReportValidator validator)
{
    public async Task<Result<ByteArrayFileContent>> DownloadAsPdfAsync(
        DownloadScheduledAppointmentsReportRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportScheduledAppointment.html", request.MapToObject());

        byte[] fileContents = htmlConverter.ConvertToPdf(html, new MemoryStream());
        return Result.File(new ByteArrayFileContent(fileContents)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName = "TotalScheduledAppointments.pdf"
        });
    }
}
