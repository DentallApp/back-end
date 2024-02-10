namespace Plugin.AppointmentReminders;

public class ReminderSettings
{
    public int ReminderTimeInAdvance { get; set; }
    public string ReminderCronExpr { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
}
