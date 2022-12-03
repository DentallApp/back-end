namespace DentallApp.Features.AppoinmentReminders;

public interface IAppoinmentReminderRepository
{
    /// <summary>
    /// Obtiene las citas agendadas que están próximas a la fecha estipulada.
    /// </summary>
    IEnumerable<AppoinmentReminderDto> GetScheduledAppoinments(int timeInAdvance, DateTime currentDate);
}
