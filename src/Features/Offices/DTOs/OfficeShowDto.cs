namespace DentallApp.Features.Offices.DTOs;

public class OfficeShowDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public bool IsDeleted { get; set; }
}
