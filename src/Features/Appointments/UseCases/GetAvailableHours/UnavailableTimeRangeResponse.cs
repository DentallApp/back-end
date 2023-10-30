namespace DentallApp.Features.Appointments.UseCases.GetAvailableHours;

public class UnavailableTimeRangeResponse
{
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
}
