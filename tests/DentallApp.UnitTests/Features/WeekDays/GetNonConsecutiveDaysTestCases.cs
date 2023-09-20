namespace DentallApp.UnitTests.Features.WeekDays;

public class GetNonConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Wednesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday} y {WeekDaysName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Monday}, {WeekDaysName.Tuesday}, {WeekDaysName.Thursday} y {WeekDaysName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDayResponse>
            {
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDaysName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDaysName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDaysName.Friday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDaysName.Saturday }
            },
            $"{WeekDaysName.Tuesday}, {WeekDaysName.Thursday}, {WeekDaysName.Friday} y {WeekDaysName.Saturday}"
        };
    }
}
