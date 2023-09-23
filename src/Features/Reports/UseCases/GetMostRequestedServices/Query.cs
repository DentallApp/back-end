namespace DentallApp.Features.Reports.UseCases.GetMostRequestedServices;

public class GetMostRequestedServicesRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
}

public class GetMostRequestedServicesResponse
{
    public string DentalServiceName { get; init; }
    public int TotalAppointmentsAssisted { get; init; }
}

public class GetMostRequestedServicesUseCase
{
    private readonly AppDbContext _context;

    public GetMostRequestedServicesUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetMostRequestedServicesResponse>> ExecuteAsync(GetMostRequestedServicesRequest request)
    {
        var appointments = await _context.Set<Appointment>()
            .Where(appointment =>
                  appointment.AppointmentStatusId == AppointmentStatusId.Assisted &&
                  appointment.Date >= request.From && appointment.Date <= request.To)
            .OptionalWhere(request.OfficeId, appointment => appointment.OfficeId == request.OfficeId)
            .GroupBy(appointment => new
            {
                appointment.GeneralTreatmentId,
                appointment.GeneralTreatment.Name
            })
            .Select(group => new GetMostRequestedServicesResponse
            {
                DentalServiceName = group.Key.Name,
                TotalAppointmentsAssisted = group.Count()
            })
            .OrderByDescending(dto => dto.TotalAppointmentsAssisted)
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return appointments;
    }
}
