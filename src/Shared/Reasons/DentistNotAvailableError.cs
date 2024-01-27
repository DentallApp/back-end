namespace DentallApp.Shared.Reasons;

public class DentistNotAvailableError
{
    public string Message { get; }
    public DentistNotAvailableError(string weekDayName)
        => Message = string.Format(Messages.DentistNotAvailable, weekDayName ?? string.Empty);
}
