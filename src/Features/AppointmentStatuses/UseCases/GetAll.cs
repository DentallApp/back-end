namespace DentallApp.Features.AppointmentStatuses.UseCases;

public class GetAppointmentStatusesResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetAppointmentStatusesUseCase
{
    private readonly AppDbContext _context;

    public GetAppointmentStatusesUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAppointmentStatusesResponse>> ExecuteAsync()
    {
        var statuses = await _context.Set<AppointmentStatus>()
            .Select(appointmentStatus => new GetAppointmentStatusesResponse
            {
                Id   = appointmentStatus.Id,
                Name = appointmentStatus.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return statuses;
    }
}
