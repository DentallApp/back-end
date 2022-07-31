namespace DentallApp.Features.Dependents.DTOs;

public class DependentUpdateDto
{
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
    public int GenderId { get; set; }
    public int KinshipId { get; set; }
    public string Email { get; set; }
}
