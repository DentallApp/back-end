namespace DentallApp.Features.Employees;

public class Employee : ModelWithSoftDelete 
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
}
