namespace DentallApp.Helpers.DateTimeHelpers;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
