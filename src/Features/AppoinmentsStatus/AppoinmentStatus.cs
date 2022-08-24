namespace DentallApp.Features.AppoinmentsStatus;

public class AppoinmentStatus : ModelBase
{
    public string Name { get; set; }
    public ICollection<Appoinment> Appoinments { get; set; }
}
