using LinqToDB;

namespace DentallApp.Features.Appointments;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentRepository(AppDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    private async Task<int> CancelAppointments(int officeId, int dentistId, IEnumerable<int> appointmentsId)
    {
        if (appointmentsId.Any())
        {
            int updatedRows = await _context.Set<Appointment>()
                .OptionalWhere(officeId, appointment => appointment.OfficeId == officeId)
                .OptionalWhere(dentistId, appointment => appointment.DentistId == dentistId)
                .Where(appointment =>
                       appointment.AppointmentStatusId == AppointmentStatusId.Scheduled &&
                       appointmentsId.Contains(appointment.Id))
                .Set(appointment => appointment.AppointmentStatusId, AppointmentStatusId.Canceled)
                .Set(appointment => appointment.IsCancelledByEmployee, true)
                .Set(appointment => appointment.UpdatedAt, _dateTimeProvider.Now)
                .UpdateAsync();

            return updatedRows;
        }

        return default;
    }

    public Task<int> CancelAppointmentsByOfficeId(int officeId, IEnumerable<int> appointmentsId)
    {
        return CancelAppointments(officeId, dentistId: default, appointmentsId);
    }

    public Task<int> CancelAppointmentsByDentistId(int dentistId, IEnumerable<int> appointmentsId)
    {
        return CancelAppointments(officeId: default, dentistId, appointmentsId);
    }
}
