namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentScheduledGetByEmployeeDto : AppoinmentPersonDto 
{
    public int DentistId { get; set; }
    public string DentistName { get; set; }
}
