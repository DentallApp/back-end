namespace DentallApp.Core.Reports.UseCases.GetMostRequestedServices;

public class GetMostRequestedServicesRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
}

public class GetMostRequestedServicesValidator : AbstractValidator<GetMostRequestedServicesRequest>
{
    public GetMostRequestedServicesValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.OfficeId).GreaterThanOrEqualTo(0);
    }
}

public class GetMostRequestedServicesResponse
{
    public string DentalServiceName { get; init; }
    public int TotalAppointmentsAssisted { get; init; }
}

public class GetMostRequestedServicesUseCase(
    DbContext context, 
    GetMostRequestedServicesValidator validator)
{
    public async Task<ListedResult<GetMostRequestedServicesResponse>> ExecuteAsync(
        GetMostRequestedServicesRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var appointments = await context.Set<Appointment>()
            .Where(appointment =>
                  appointment.AppointmentStatusId == (int)AppointmentStatus.Predefined.Assisted &&
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

        return Result.ObtainedResources(appointments);
    }
}
