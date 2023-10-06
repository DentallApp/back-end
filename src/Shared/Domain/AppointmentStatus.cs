namespace DentallApp.Shared.Domain;

public class AppointmentStatus : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
