namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentGetByEmployeeDto : AppointmentPersonDto
{
    public string Status { get; set; }
    public int StatusId { get; set; }
    public int DentistId { get; set; }
    public string DentistName { get; set; }
    public string OfficeName { get; set; }
}
