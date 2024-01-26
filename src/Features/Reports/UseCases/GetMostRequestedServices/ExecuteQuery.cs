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

public class GetMostRequestedServicesUseCase(DbContext context)
{
    public async Task<IEnumerable<GetMostRequestedServicesResponse>> ExecuteAsync(GetMostRequestedServicesRequest request)
    {
        var appointments = await context.Set<Appointment>()
            .Where(appointment =>
                  appointment.AppointmentStatusId == (int)StatusOfAppointment.Assisted &&
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
