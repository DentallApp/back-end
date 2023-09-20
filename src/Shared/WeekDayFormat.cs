namespace DentallApp.Shared;

public class WeekDayFormat
{
    /// <summary>
    /// Comprueba sí la colección tiene días consecutivos.
    /// </summary>
    /// <param name="weekDays">
    /// Una colección con los días de la semana (la colección debe estar ordenada de forma ascendente).
    /// </param>
    /// <returns><c>true</c> sí la colección tiene días consecutivos; de lo contrario, <c>false</c>.</returns>
    private static bool HasConsecutiveDays(List<WeekDayResponse> weekDays)
    {
        for(int i = 1, len = weekDays.Count; i < len; i++)
            if (weekDays[i].Id - weekDays[i - 1].Id != 1)
                return false;
        return true;
    }

    /// <summary>
    /// Obtiene los días de las semanas en un formato.
    /// </summary>
    /// <param name="weekDays">
    /// Una colección con los días de la semana (la colección debe estar ordenada de forma ascendente).
    /// </param>
    /// <remarks>Ejemplo: Lunes a Viernes / Lunes, Miércoles y Viernes.</remarks>
    public static string GetWeekDaysFormat(List<WeekDayResponse> weekDays)
    {
        var weekDaysCount = weekDays.Count;
        if (weekDaysCount == 0)
            return string.Empty;

        if (weekDaysCount == 1)
            return weekDays[0].Name;

        if (weekDaysCount == 2)
            return $"{weekDays[0].Name} y {weekDays[1].Name}";

        if (HasConsecutiveDays(weekDays))
            return $"{weekDays[0].Name} a {weekDays[^1].Name}";

        string format = "";
        int i;
        weekDaysCount -= 2;
        for (i = 0; i < weekDaysCount; i++)
            format += weekDays[i].Name + ", ";
        return $"{format}{weekDays[i].Name} y {weekDays[^1].Name}";
    }
}
