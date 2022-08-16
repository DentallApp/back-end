namespace DentallApp.Features.Offices;

public class Office : ModelWithSoftDelete
{
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
