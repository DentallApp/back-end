namespace DentallApp.UnitTests.Features.WeekDays;

public class GetNonConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Wednesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Saturday,  WeekDayName = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday}, {WeekDaysName.Thursday} y {WeekDaysName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday },
                new() { WeekDayId = (int)DayOfWeek.Saturday,  WeekDayName = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Tuesday}, {WeekDaysName.Thursday}, {WeekDaysName.Friday} y {WeekDaysName.Saturday}"
        };
    }
}
