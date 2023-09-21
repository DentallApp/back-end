namespace DentallApp.UnitTests.Shared.WeekDays;

public class GetConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Wednesday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Monday} a {WeekDaysName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Tuesday} a {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Wednesday} a {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Thursday, Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,   Name = WeekDaysName.Friday },
                new() { Id = (int)DayOfWeek.Saturday, Name = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Thursday} a {WeekDaysName.Saturday}"
        };
    }
}
