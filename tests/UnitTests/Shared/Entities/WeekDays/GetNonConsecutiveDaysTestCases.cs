namespace UnitTests.Shared.Entities.WeekDays;

public class GetNonConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday }
            },
            $"{WeekDayName.Monday}, {WeekDayName.Wednesday} y {WeekDayName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday }
            },
            $"{WeekDayName.Monday}, {WeekDayName.Tuesday} y {WeekDayName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday }
            },
            $"{WeekDayName.Monday}, {WeekDayName.Tuesday} y {WeekDayName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDayName.Saturday }
            },
            $"{WeekDayName.Monday}, {WeekDayName.Tuesday}, {WeekDayName.Thursday} y {WeekDayName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDayName.Saturday }
            },
            $"{WeekDayName.Tuesday}, {WeekDayName.Thursday}, {WeekDayName.Friday} y {WeekDayName.Saturday}"
        };
    }
}
