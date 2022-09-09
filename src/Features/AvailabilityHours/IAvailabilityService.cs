namespace DentallApp.Features.AvailabilityHours;

public interface IAvailabilityService
{
    Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangePostDto);
}
