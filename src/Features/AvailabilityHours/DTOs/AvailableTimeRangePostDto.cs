namespace DentallApp.Features.AvailabilityHours.DTOs;

public class AvailableTimeRangePostDto
{
    public int OfficeId { get; set; }
    public int DentistId { get; set; }
    public int DentalServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
}
