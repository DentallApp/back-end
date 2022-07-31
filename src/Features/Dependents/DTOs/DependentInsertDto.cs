namespace DentallApp.Features.Dependents.DTOs;

public class DependentInsertDto : PersonDto
{
    public string Email { get; set; }
    public int KinshipId { get; set; }
}
