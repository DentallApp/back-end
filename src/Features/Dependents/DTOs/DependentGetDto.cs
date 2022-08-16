namespace DentallApp.Features.Dependents.DTOs;

public class DependentGetDto : PersonDto
{
    public int DependentId { get; set; }
    public string Email { get; set; }
    public string GenderName { get; set; }
    public string KinshipName { get; set; }
    public int KinshipId { get; set; }
}
