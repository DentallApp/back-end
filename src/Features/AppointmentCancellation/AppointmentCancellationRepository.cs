using LinqToDB;

namespace DentallApp.Features.AppointmentCancellation;

public class AppointmentCancellationRepository : Repository<Appointment>, IAppointmentCancellationRepository
{
    public AppointmentCancellationRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Cancela las citas por parte del empleado.
    /// </summary>
    private async Task<int> CancelAppointmentsForEmployeeAsync(int officeId, int dentistId, IEnumerable<int> appointmentsId)
        => appointmentsId.Count() == 0 ? default :
           await Context.Set<Appointment>()
                        .OptionalWhere(officeId, appointment => appointment.OfficeId == officeId)
                        .OptionalWhere(dentistId, appointment => appointment.DentistId == dentistId)
                        .Where(appointment =>
                               appointment.AppointmentStatusId == AppointmentStatusId.Scheduled &&
                               appointmentsId.Contains(appointment.Id))
                        .Set(appointment => appointment.AppointmentStatusId, AppointmentStatusId.Canceled)
                        .Set(appointment => appointment.IsCancelledByEmployee, true)
                        .Set(appointment => appointment.UpdatedAt, DateTime.Now)
                        .UpdateAsync();

    public async Task<int> CancelAppointmentsByOfficeIdAsync(int officeId, IEnumerable<int> appointmentsId)
        => await CancelAppointmentsForEmployeeAsync(officeId, dentistId: default, appointmentsId);

    public async Task<int> CancelAppointmentsByDentistIdAsync(int dentistId, IEnumerable<int> appointmentsId)
        => await CancelAppointmentsForEmployeeAsync(officeId: default, dentistId, appointmentsId);
}
