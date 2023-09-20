namespace DentallApp.UnitTests.Features.WeekDays;

public partial class WeekDayFormatTests
{
    [Test]
    public void GetWeekDaysFormat_WhenWeekDaysIsZero_ShouldReturnsStringEmpty()
    {
        // Arrange
        var weekDays = new List<WeekDayResponse>();
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
        var weekDays = new List<WeekDayResponse>() 
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
        var weekDays = new List<WeekDayResponse>()
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
    public void GetWeekDaysFormat_WhenDaysAreConsecutive_ShouldReturnsNewFormat(List<WeekDayResponse> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestCaseSource(typeof(GetNonConsecutiveDaysTestCases))]
    public void GetWeekDaysFormat_WhenDaysAreNotConsecutive_ShouldReturnsNewFormat(List<WeekDayResponse> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }
}
