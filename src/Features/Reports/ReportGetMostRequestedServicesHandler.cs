namespace DentallApp.Features.Reports;

public class ReportGetMostRequestedServicesRequest : IRequest<IEnumerable<ReportGetMostRequestedServicesResponse>>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int OfficeId { get; set; }
}

public class ReportGetMostRequestedServicesHandler
    : IRequestHandler<ReportGetMostRequestedServicesRequest, IEnumerable<ReportGetMostRequestedServicesResponse>>
{
    private readonly AppDbContext _context;

    public ReportGetMostRequestedServicesHandler(AppDbContext context)
        => _context = context;

    public async Task<IEnumerable<ReportGetMostRequestedServicesResponse>> Handle(ReportGetMostRequestedServicesRequest request, CancellationToken cancellationToken)
        => await _context.Set<Appointment>()
                         .Include(appointment => appointment.GeneralTreatment)
                         .Where(appointment =>
                               (appointment.AppointmentStatusId == AppointmentStatusId.Assisted) &&
                               (appointment.Date >= request.From && appointment.Date <= request.To))
                         .OptionalWhere(request.OfficeId, appointment => appointment.OfficeId == request.OfficeId)
                         .GroupBy(appointment => new { appointment.GeneralTreatmentId, appointment.GeneralTreatment.Name })
                         .Select(group => new ReportGetMostRequestedServicesResponse
                         {
                             DentalServiceName = group.Key.Name,
                             TotalAppointmentsAssisted = group.Count()
                         })
                         .OrderByDescending(dto => dto.TotalAppointmentsAssisted)
                         .IgnoreQueryFilters()
                         .ToListAsync();
}
