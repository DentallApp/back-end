namespace UnitTests.Shared.Entities.WeekDays;

public class GetConsecutiveDaysTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday }
            },
            $"{WeekDayName.Monday} a {WeekDayName.Wednesday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday }
            },
            $"{WeekDayName.Monday} a {WeekDayName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday }
            },
            $"{WeekDayName.Monday} a {WeekDayName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Monday,    Name = WeekDayName.Monday },
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday },
                new() { Id = (int)DayOfWeek.Saturday,  Name = WeekDayName.Saturday }
            },
            $"{WeekDayName.Monday} a {WeekDayName.Saturday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Tuesday,   Name = WeekDayName.Tuesday },
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday }
            },
            $"{WeekDayName.Tuesday} a {WeekDayName.Thursday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDayName.Wednesday },
                new() { Id = (int)DayOfWeek.Thursday,  Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,    Name = WeekDayName.Friday }
            },
            $"{WeekDayName.Wednesday} a {WeekDayName.Friday}"
        };

        yield return new object[]
        {
            new List<WeekDay>
            {
                new() { Id = (int)DayOfWeek.Thursday, Name = WeekDayName.Thursday },
                new() { Id = (int)DayOfWeek.Friday,   Name = WeekDayName.Friday },
                new() { Id = (int)DayOfWeek.Saturday, Name = WeekDayName.Saturday }
            },
            $"{WeekDayName.Thursday} a {WeekDayName.Saturday}"
        };
    }
}
