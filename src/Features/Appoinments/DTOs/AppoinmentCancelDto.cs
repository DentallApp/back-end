namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentCancelDto
{
    public string Reason { get; set; }
    public IEnumerable<AppoinmentCancelDetailsDto> Appoinments { get; set; }
}
