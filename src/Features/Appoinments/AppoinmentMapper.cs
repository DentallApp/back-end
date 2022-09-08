namespace DentallApp.Features.Appoinments;

public static class AppoinmentMapper
{
    [Decompile]
    public static UnavailableTimeRangeDto MapToUnavailableTimeRangeDto(this Appoinment appoinment)
        => new()
        {
            StartHour             = appoinment.StartHour,
            EndHour               = appoinment.EndHour
        };
}
