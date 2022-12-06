namespace DentallApp.Features.AppointmentReminders;

public class AppointmentReminderDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; }
    public string PatientCellPhone { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public string DentistName { get; set; }
}
