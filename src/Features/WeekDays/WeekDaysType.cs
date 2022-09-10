namespace DentallApp.Features.WeekDays;

public class WeekDaysType
{
    public const int MaxWeekDay = 6;

    public static Dictionary<int, string> WeekDays => new()
    {
        {0, WeekDaysName.Sunday },
        {1, WeekDaysName.Monday },
        {2, WeekDaysName.Tuesday },
        {3, WeekDaysName.Wednesday },
        {4, WeekDaysName.Thursday },
        {5, WeekDaysName.Friday },
        {6, WeekDaysName.Saturday }
    };
}

public class WeekDaysName
{
    public const string Monday      = "Lunes";
    public const string Tuesday     = "Martes";
    public const string Wednesday   = "Miércoles";
    public const string Thursday    = "Jueves";
    public const string Friday      = "Viernes";
    public const string Saturday    = "Sábado";
    public const string Sunday      = "Domingo";
}
