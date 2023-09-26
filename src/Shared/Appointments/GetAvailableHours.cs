namespace DentallApp.Shared.Appointments;

public class AvailableTimeRangeRequest
{
    public int OfficeId { get; init; }
    public int DentistId { get; init; }
    public int DentalServiceId { get; init; }
    public DateTime AppointmentDate { get; init; }
}

public class AvailableTimeRangeResponse
{
    public string StartHour { get; init; }
    public string EndHour { get; init; }

    public override string ToString()
    {
        return $"{StartHour} - {EndHour}";
    }
}

/// <summary>
/// This interface is used so that the chatbot is not directly coupled 
/// to the <see cref="Features.Appointments" /> module.
/// </summary>
public interface IGetAvailableHoursUseCase
{
    Task<Response<IEnumerable<AvailableTimeRangeResponse>>> ExecuteAsync(AvailableTimeRangeRequest request);
}
