namespace DentallApp.Features.AppoinmentReminders;

public interface IAppoinmentReminderRepository
{
    /// <summary>
    /// Obtiene las citas agendadas que están próximas a la fecha estipulada.
    /// </summary>
    IEnumerable<AppoinmentReminderDto> GetScheduledAppoinments(int timeInAdvance, DateTime currentDate);

    /// <summary>
    /// Actualiza las citas agendadas para garantizar que el paciente reciba únicamente un recordatorio.
    /// </summary>
    void UpdateScheduledAppoinments(IEnumerable<int> appoinmentsId);
}
