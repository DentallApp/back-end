namespace DentallApp.UnitTests.Features.WeekDays;

public partial class WeekDayFormatTests
{
    [Test]
    public void GetWeekDaysFormat_WhenWeekDaysIsZero_ShouldReturnsStringEmpty()
    {
        // Arrange
        var weekDays = new List<WeekDayDto>();
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
        var weekDays = new List<WeekDayDto>() 
        {
            new() { WeekDayId = (int)DayOfWeek.Monday, WeekDayName = WeekDaysName.Monday }
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
        var weekDays = new List<WeekDayDto>()
        {
            new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
            new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday }
        };
        var expected = $"{WeekDaysName.Monday} y {WeekDaysName.Wednesday}";

        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetConsecutiveDaysTestCases))]
    public void GetWeekDaysFormat_WhenDaysAreConsecutive_ShouldReturnsNewFormat(List<WeekDayDto> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetNonConsecutiveDaysTestCases))]
    public void GetWeekDaysFormat_WhenDaysAreNotConsecutive_ShouldReturnsNewFormat(List<WeekDayDto> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }
}
