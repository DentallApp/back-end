namespace DentallApp.Features.Employees.DTOs;

public class FullEmployeeProfileDto : FullUserProfileDto
{
    public int EmployeeId { get; set; }
    public int OfficeId { get; set; }
    public string OfficeName { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
}
