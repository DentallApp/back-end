namespace DentallApp.Features.AppointmentReminders;

public class ReminderSettings
{
    public int ReminderTimeInAdvance { get; set; }
    public string ReminderCronExpr { get; set; }
    public string BusinessName { get; set; }
}
