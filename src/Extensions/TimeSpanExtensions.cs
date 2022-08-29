namespace DentallApp.Extensions;

public static class TimeSpanExtensions
{
    public static string GetHourWithoutSeconds(this TimeSpan timeSpan)
        => timeSpan.ToString(@"hh\:mm");
}
