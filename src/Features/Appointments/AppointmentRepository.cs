namespace DentallApp.Features.Appointments;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentRepository(IDateTimeProvider dateTimeProvider, AppDbContext context) : base(context) 
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate)
        => await Context.Set<Appointment>()
                        .Where(appointment => 
                              (appointment.DentistId == dentistId) &&
                              (appointment.Date == appointmentDate) &&
                              (appointment.IsNotCanceled() ||
                               appointment.IsCancelledByEmployee ||
                               // Checks if the canceled appointment is not available.
                               // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                               _dateTimeProvider.Now > Context.AddTime(Context.ToDateTime(appointment.Date), appointment.StartHour)))
                        .Select(appointment => appointment.MapToUnavailableTimeRangeDto())
                        .Distinct()
                        .OrderBy(appointment => appointment.StartHour)
                          .ThenBy(appointment => appointment.EndHour)
                        .ToListAsync();

    public async Task<AppointmentInfoDto> GetAppointmentInformationAsync(int id)
        => await Context.Set<Appointment>()
                        .Include(appointment => appointment.Person)
                        .Include(appointment => appointment.Employee.Person)
                        .Include(appointment => appointment.Office)
                        .Include(appointment => appointment.GeneralTreatment)
                        .Where(appointment => appointment.Id == id)
                        .Select(appointment => appointment.MapToAppointmentInfoDto())
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();
}
