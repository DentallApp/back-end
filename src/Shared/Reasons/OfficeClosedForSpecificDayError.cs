namespace DentallApp.Shared.Reasons;

public class OfficeClosedForSpecificDayError
{
    public string Message { get; }
    public OfficeClosedForSpecificDayError(string weekDayName)
        => Message = string.Format(Messages.OfficeClosedForSpecificDay, weekDayName ?? string.Empty);
}
