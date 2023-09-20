namespace DentallApp.UnitTests.Features.WeekDays;

public class GetConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Wednesday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday },
                new() { WeekDayId = (int)DayOfWeek.Saturday,  WeekDayName = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Tuesday} a {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Wednesday} a {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Thursday, WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,   WeekDayName = WeekDaysName.Friday },
                new() { WeekDayId = (int)DayOfWeek.Saturday, WeekDayName = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Thursday} a {WeekDaysName.Saturday}"
        };
    }
}
