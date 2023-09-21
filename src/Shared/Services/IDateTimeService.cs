namespace DentallApp.Shared.Services;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
