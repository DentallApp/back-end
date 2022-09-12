namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentGetByEmployeeDto : AppoinmentPersonDto
{
    public string Status { get; set; }
    public int StatusId { get; set; }
    public int DentistId { get; set; }
    public string DentistName { get; set; }
}
