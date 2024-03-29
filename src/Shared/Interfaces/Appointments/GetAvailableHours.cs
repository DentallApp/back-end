﻿namespace DentallApp.Shared.Interfaces.Appointments;

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
/// to the <c>Appointments</c> module.
/// </summary>
public interface IGetAvailableHoursUseCase
{
    Task<ListedResult<AvailableTimeRangeResponse>> ExecuteAsync(AvailableTimeRangeRequest request);
}
