namespace DentallApp.Features.AvailabilityHours.Models;

public class AvailableTimeRangeRequest
{
    public int OfficeId { get; init; }
    public int DentistId { get; init; }
    public int DentalServiceId { get; init; }
    public DateTime AppointmentDate { get; init; }
}
