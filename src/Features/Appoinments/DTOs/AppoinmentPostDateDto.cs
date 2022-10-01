namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentPostDateDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int OfficeId { get; set; }
    public int DentistId { get; set; }
    public int StatusId { get; set; }
}