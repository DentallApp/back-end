namespace DentallApp.Features.Appointments.UseCases;

public class UnavailableTimeRangeResponse
{
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
}

public interface IGetUnavailableHoursUseCase
{
    Task<List<UnavailableTimeRangeResponse>> Execute(int dentistId, DateTime appointmentDate);
}

public class GetUnavailableHoursUseCase : IGetUnavailableHoursUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public GetUnavailableHoursUseCase(AppDbContext context, IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<List<UnavailableTimeRangeResponse>> Execute(int dentistId, DateTime appointmentDate)
    {
        var response = await _context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == dentistId) &&
                  (appointment.Date == appointmentDate) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   _dateTimeService.Now > _context.AddTime(_context.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => new UnavailableTimeRangeResponse
            {
                StartHour = appointment.StartHour,
                EndHour   = appointment.EndHour
            })
            .Distinct()
            .OrderBy(appointment => appointment.StartHour)
              .ThenBy(appointment => appointment.EndHour)
            .AsNoTracking()
            .ToListAsync();

        return response;
    }
}
