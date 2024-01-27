namespace DentallApp.Shared.Reasons;

public class TotalHoursAvailableSuccess
{
    public string Message { get; }
    public TotalHoursAvailableSuccess(int totalHours)
        => Message = string.Format(Messages.TotalHoursAvailable, totalHours);
}
