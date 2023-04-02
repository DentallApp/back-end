namespace DentallApp.Extensions;

public static class DateTimeExtensions
{
    public static string GetDateInSpanishFormat(this DateTime dateTime)
        => dateTime.ToString("D", new CultureInfo("es-ES"));

    public static string GetDateAndHourInSpanishFormat(this DateTime dateTime)
        => dateTime.ToString("f", new CultureInfo("es-ES"));

    public static string GetDateWithStandardFormat(this DateTime dateTime)
        => dateTime.ToString("yyyy-MM-dd");

    public static string GetDateAndHourInSpanishFormat(this DateTime? dateTime)
        => dateTime?.ToString("f", new CultureInfo("es-ES"));
}
