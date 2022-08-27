namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentGetByEmployeeDto
{
    public string Document { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
    public DateTime DateBirth { get; set; }
    public int Age { get; set; }
}
