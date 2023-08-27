namespace DentallApp.Features.Appointments;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentRepository(IDateTimeProvider dateTimeProvider, AppDbContext context)
    {
        _dateTimeProvider = dateTimeProvider;
        _context = context;
    }

    public async Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate)
    {
        var response = await _context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == dentistId) &&
                  (appointment.Date == appointmentDate) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   _dateTimeProvider.Now > _context.AddTime(_context.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => appointment.MapToUnavailableTimeRangeDto())
            .Distinct()
            .OrderBy(appointment => appointment.StartHour)
              .ThenBy(appointment => appointment.EndHour)
            .AsNoTracking()
            .ToListAsync();

        return response;
    }
}
