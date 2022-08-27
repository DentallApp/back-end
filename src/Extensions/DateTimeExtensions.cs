namespace DentallApp.Extensions;

public static class DateTimeExtensions
{
    public static string GetDateInSpanishFormat(this DateTime dateTime)
        => dateTime.ToString("D", new System.Globalization.CultureInfo("es-ES"));

    public static string GetDateAndHourInSpanishFormat(this DateTime dateTime)
        => dateTime.ToString("f", new System.Globalization.CultureInfo("es-ES"));

    public static string GetDateAndHourInSpanishFormat(this DateTime? dateTimeNullable)
    {
        if (dateTimeNullable is null) 
            return null;
        DateTime dt = (DateTime)dateTimeNullable;
        return dt.ToString("f", new System.Globalization.CultureInfo("es-ES"));
    }
}
