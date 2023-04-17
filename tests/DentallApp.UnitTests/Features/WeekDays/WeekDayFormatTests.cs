namespace DentallApp.UnitTests.Features.WeekDays;

[TestClass]
public partial class WeekDayFormatTests
{
    [TestMethod]
    public void GetWeekDaysFormat_WhenWeekDaysIsZero_ShouldReturnStringEmpty()
    {
        // Arrange
        var weekDays = new List<WeekDayDto>();
        var expected = string.Empty;

        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [TestMethod]
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

    [TestMethod]
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

    [DataTestMethod]
    [DynamicData(nameof(GetDataConsecutive), DynamicDataSourceType.Method)]
    public void GetWeekDaysFormat_WhenDaysAreConsecutive_ShouldReturnsNewFormat(string testId, List<WeekDayDto> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetDataNotConsecutive), DynamicDataSourceType.Method)]
    public void GetWeekDaysFormat_WhenDaysAreNotConsecutive_ShouldReturnsNewFormat(string testId, List<WeekDayDto> weekDays, string expected)
    {
        // Act
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        // Assert
        format.Should().Be(expected);
    }
}
