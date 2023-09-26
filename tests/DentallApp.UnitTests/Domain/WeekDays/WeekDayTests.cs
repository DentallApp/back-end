namespace DentallApp.UnitTests.Domain.WeekDays;

public class WeekDayTests
{
    [Test]
    public void ConvertToDayRange_WhenWeekDaysIsZero_ShouldReturnsStringEmpty()
    {
        // Arrange
        var weekDays = new List<WeekDay>();
        var expected = string.Empty;

        // Act
        var actual = WeekDay.ConvertToDayRange(weekDays);

        // Assert
        actual.Should().Be(expected);
    }

    [Test]
    public void ConvertToDayRange_WhenWeekDaysIsOne_ShouldReturnsNewFormat()
    {
        // Arrange
        var weekDays = new List<WeekDay>()
        {
            new() { Id = (int)DayOfWeek.Monday, Name = WeekDaysName.Monday }
        };
        var expected = WeekDaysName.Monday;

        // Act
        var actual = WeekDay.ConvertToDayRange(weekDays);

        // Assert
        actual.Should().Be(expected);
    }

    [Test]
    public void ConvertToDayRange_WhenWeekDaysIsTwo_ShouldReturnsNewFormat()
    {
        // Arrange
        var weekDays = new List<WeekDay>()
        {
            new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
            new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday }
        };
        var expected = $"{WeekDaysName.Monday} y {WeekDaysName.Wednesday}";

        // Act
        var actual = WeekDay.ConvertToDayRange(weekDays);

        // Assert
        actual.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetConsecutiveDaysTestCases))]
    public void ConvertToDayRange_WhenDaysAreConsecutive_ShouldReturnsNewFormat(
        List<WeekDay> weekDays, 
        string expected)
    {
        // Act
        var actual = WeekDay.ConvertToDayRange(weekDays);

        // Assert
        actual.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetNonConsecutiveDaysTestCases))]
    public void ConvertToDayRange_WhenDaysAreNotConsecutive_ShouldReturnsNewFormat(
        List<WeekDay> weekDays, 
        string expected)
    {
        // Act
        var actual = WeekDay.ConvertToDayRange(weekDays);

        // Assert
        actual.Should().Be(expected);
    }
}
