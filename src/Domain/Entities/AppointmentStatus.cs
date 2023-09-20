namespace DentallApp.Domain.Entities;

public class AppointmentStatus : EntityBase
{
    public string Name { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
