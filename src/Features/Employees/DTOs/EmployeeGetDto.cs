namespace DentallApp.Features.Employees.DTOs;

public class EmployeeGetDto
{
    public int EmployeeId { get; set; }
    public int OfficeId { get; set; }
    public string OfficeName { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string Email { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
    public int? GenderId { get; set; }
    public string GenderName { get; set; }
    public IEnumerable<RoleGetDto> Roles { get; set; }
    public IEnumerable<GeneralTreatmentGetNameDto> Specialties { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
}
