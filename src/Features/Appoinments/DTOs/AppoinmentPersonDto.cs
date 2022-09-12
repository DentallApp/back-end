namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentPersonDto : AppoinmentGetDto
{
    public string Document { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
    public DateTime? DateBirth { get; set; }
}
