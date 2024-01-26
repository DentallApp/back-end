namespace DentallApp.Shared.Domain;

public class AppointmentStatus : BaseEntity
{
    public string Name { get; set; }
}

public enum StatusOfAppointment
{
    Scheduled = 1,
    Assisted,
    NotAssisted,
    Canceled
}
