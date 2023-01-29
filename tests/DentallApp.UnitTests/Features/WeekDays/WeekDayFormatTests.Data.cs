namespace DentallApp.UnitTests.Features.WeekDays;

public partial class WeekDayFormatTests
{
    public static IEnumerable<object[]> GetDataConsecutive()
    {
        yield return new object[]
        {
            "test1",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Wednesday}"
        };

        yield return new object[]
        {
            "test2",
            new List<WeekDayDto>
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
            "test3",
            new List<WeekDayDto>
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
            "test4",
            new List<WeekDayDto>
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
            "test5",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Tuesday} a {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            "test6",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Wednesday} a {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            "test7",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Thursday, WeekDayName = WeekDaysName.Thursday },
                new() { WeekDayId = (int)DayOfWeek.Friday,   WeekDayName = WeekDaysName.Friday },
                new() { WeekDayId = (int)DayOfWeek.Saturday, WeekDayName = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Thursday} a {WeekDaysName.Saturday}"
        };
    }

    public static IEnumerable<object[]> GetDataNotConsecutive()
    {
        yield return new object[]
        {
            "test1",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Wednesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            "test2",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Friday,    WeekDayName = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            "test3",
            new List<WeekDayDto>
            {
                new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
                new() { WeekDayId = (int)DayOfWeek.Tuesday,   WeekDayName = WeekDaysName.Tuesday },
                new() { WeekDayId = (int)DayOfWeek.Thursday,  WeekDayName = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            "test4",
            new List<WeekDayDto>
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
            "test5",
            new List<WeekDayDto>
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
