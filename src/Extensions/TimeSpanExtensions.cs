namespace DentallApp.Extensions;

public static class TimeSpanExtensions
{
    public static string GetHourWithoutSeconds(this TimeSpan timeSpan)
        => timeSpan.ToString(@"hh\:mm");

    public static string GetHourWithoutSeconds(this TimeSpan? timeSpanNullable)
    {
        if (timeSpanNullable is null)
            return null;
        TimeSpan ts = (TimeSpan)timeSpanNullable;
        return ts.ToString(@"hh\:mm");
    }
}
