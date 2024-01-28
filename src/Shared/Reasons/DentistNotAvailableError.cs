namespace DentallApp.Shared.Reasons;

public readonly ref struct DentistNotAvailableError
{
    public string Message { get; }
    public DentistNotAvailableError(string weekDayName)
        => Message = string.Format(Messages.DentistNotAvailable, weekDayName ?? string.Empty);
}
