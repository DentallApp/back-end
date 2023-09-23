namespace DentallApp.UnitTests.Shared.WeekDays;

public partial class WeekDayFormatTests
{
    [Test]
    public void GetWeekDaysFormat_WhenWeekDaysIsZero_ShouldReturnsStringEmpty()
    {
        // Arrange
        var weekDays = new List<WeekDay>();
        var expected = string.Empty;

        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [Test]
    public void GetWeekDaysFormat_WhenWeekDaysIsOne_ShouldReturnsNewFormat()
    {
        // Arrange
        var weekDays = new List<WeekDay>()
        {
            new() { Id = (int)DayOfWeek.Monday, Name = WeekDaysName.Monday }
        };
        var expected = WeekDaysName.Monday;

        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [Test]
    public void GetWeekDaysFormat_WhenWeekDaysIsTwo_ShouldReturnsNewFormat()
    {
        // Arrange
        var weekDays = new List<WeekDay>()
        {
            new() { Id = (int)DayOfWeek.Monday,    Name = WeekDaysName.Monday },
            new() { Id = (int)DayOfWeek.Wednesday, Name = WeekDaysName.Wednesday }
        };
        var expected = $"{WeekDaysName.Monday} y {WeekDaysName.Wednesday}";

        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetConsecutiveDaysTestCases))]
    public void GetWeekDaysFormat_WhenDaysAreConsecutive_ShouldReturnsNewFormat(List<WeekDay> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetNonConsecutiveDaysTestCases))]
    public void GetWeekDaysFormat_WhenDaysAreNotConsecutive_ShouldReturnsNewFormat(List<WeekDay> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }
}
