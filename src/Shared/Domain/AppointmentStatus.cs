namespace DentallApp.Shared.Domain;

public class AppointmentStatus : BaseEntity
{
    public string Name { get; set; }

    /// <summary>
    /// Specifies the predefined status.
    /// </summary>
    public enum Predefined
    {
        Scheduled = 1,
        Assisted,
        NotAssisted,
        Canceled
    }
}
