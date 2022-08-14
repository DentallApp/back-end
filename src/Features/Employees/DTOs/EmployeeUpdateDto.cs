namespace DentallApp.Features.Employees.DTOs;

public class EmployeeUpdateDto
{
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
    public int GenderId { get; set; }
}
