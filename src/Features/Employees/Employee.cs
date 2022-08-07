namespace DentallApp.Features.Employees;

public class Employee : ModelWithStatus 
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
}
