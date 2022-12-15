namespace DentallApp.Models;

public class AppointmentStatus : ModelBase
{
    public string Name { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
