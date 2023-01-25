namespace DentallApp.Features.AppointmentReminders;

public interface IAppointmentReminderQueries
{
    /// <summary>
    /// Obtiene las citas agendadas que están próximas a la fecha estipulada.
    /// </summary>
    IEnumerable<AppointmentReminderDto> GetScheduledAppointments(int timeInAdvance, DateTime currentDate);
}
