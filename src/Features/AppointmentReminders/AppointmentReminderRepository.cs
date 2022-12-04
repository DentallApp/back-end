using LinqToDB;

namespace DentallApp.Features.AppointmentReminders;

public class AppointmentReminderRepository : IAppointmentReminderRepository
{
    private readonly AppDbContext _context;

    public AppointmentReminderRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AppointmentReminderDto> GetScheduledAppointments(int timeInAdvance, DateTime currentDate)
        => _context.Set<Appointment>()
                   .Include(appointment => appointment.Person)
                   .Include(appointment => appointment.Employee)
                      .ThenInclude(employee => employee.Person)
                   .Where(appointment =>
                          appointment.AppointmentStatusId == AppointmentStatusId.Scheduled &&
                          _context.DateDiff(appointment.Date, currentDate) == timeInAdvance &&
                          appointment.CreatedAt != null &&
                          // Para que el recordatorio no se envíe sí el paciente agenda la cita para el día siguiente.
                          // El caso anterior sucede cuando el tiempo de antelación (timeInAdvance) es de 1 día.
                          currentDate != _context.GetDate(appointment.CreatedAt)
                         )
                   .Select(appointment => new AppointmentReminderDto
                    {
                        AppointmentId    = appointment.Id,
                        PatientName      = appointment.Person.FullName,
                        PatientCellPhone = appointment.Person.CellPhone,
                        Date             = appointment.Date,
                        StartHour        = appointment.StartHour,
                        DentistName      = appointment.Employee.Person.FullName
                    })
                   .IgnoreQueryFilters()
                   .ToList();
}
