namespace DentallApp.Shared.Reasons;

public readonly ref struct TotalHoursAvailableSuccess
{
    public string Message { get; }
    public TotalHoursAvailableSuccess(int totalHours)
        => Message = string.Format(Messages.TotalHoursAvailable, totalHours);
}
