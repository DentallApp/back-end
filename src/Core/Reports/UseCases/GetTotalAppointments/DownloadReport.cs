namespace DentallApp.Core.Reports.UseCases.GetTotalAppointments;

public class DownloadTotalAppointmentsReportRequest
{
    public class TotalAppointments
    {
        public int Total { get; init; }
        public int TotalAppointmentsAssisted { get; init; }
        public int TotalAppointmentsNotAssisted { get; init; }
        public int TotalAppointmentsCancelledByPatient { get; init; }
        public int TotalAppointmentsCancelledByEmployee { get; init; }
    }

    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public string OfficeName { get; init; }
    public string DentistName { get; init; }
    public TotalAppointments Totals { get; init; }

    public object MapToObject() => new
    {
        From = From.GetDateWithStandardFormat(),
        To   = To.GetDateWithStandardFormat(),
        OfficeName,
        DentistName,
        Totals
    };
}

public class DownloadTotalAppointmentsReportValidator 
    : AbstractValidator<DownloadTotalAppointmentsReportRequest>
{
    public DownloadTotalAppointmentsReportValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.OfficeName).NotEmpty();
        RuleFor(request => request.DentistName).NotEmpty();
        RuleFor(request => request.Totals)
            .NotEmpty()
            .ChildRules(validator =>
            {
                validator
                    .RuleFor(appointment => appointment.Total)
                    .GreaterThanOrEqualTo(0);

                validator
                    .RuleFor(appointment => appointment.TotalAppointmentsAssisted)
                    .GreaterThanOrEqualTo(0);

                validator
                    .RuleFor(appointment => appointment.TotalAppointmentsNotAssisted)
                    .GreaterThanOrEqualTo(0);

                validator
                    .RuleFor(appointment => appointment.TotalAppointmentsCancelledByPatient)
                    .GreaterThanOrEqualTo(0);

                validator
                    .RuleFor(appointment => appointment.TotalAppointmentsCancelledByEmployee)
                    .GreaterThanOrEqualTo(0);
            });
    }
}

public class DownloadTotalAppointmentsReportUseCase(
    IHtmlTemplateLoader htmlTemplateLoader,
    IHtmlConverter htmlConverter,
    DownloadTotalAppointmentsReportValidator validator)
{
    public async Task<Result<ByteArrayFileContent>> DownloadAsPdfAsync(
        DownloadTotalAppointmentsReportRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var html = await htmlTemplateLoader
            .LoadAsync("./Templates/ReportAppointment.html", request.MapToObject());

        byte[] fileContents = htmlConverter.ConvertToPdf(html, new MemoryStream());
        return Result.File(new ByteArrayFileContent(fileContents)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName = "TotalAppointments.pdf"
        });
    }
}
