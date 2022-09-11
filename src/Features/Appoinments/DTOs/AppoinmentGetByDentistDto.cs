namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentGetByDentistDto : AppoinmentPersonDto
{
    public string Status { get; set; }
    public int StatusId { get; set; }
}
