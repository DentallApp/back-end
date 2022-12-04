namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentPostDateDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int OfficeId { get; set; }
    public int DentistId { get; set; }
    public int StatusId { get; set; }
}