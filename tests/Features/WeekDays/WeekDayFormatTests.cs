namespace DentallApp.Tests.Features.WeekDays;

[TestClass]
public partial class WeekDayFormatTests
{
    [TestMethod]
    public void GetWeekDaysFormat_WhenWeekDaysIsZero_ShouldReturnStringEmpty()
    {
        var weekDays = new List<WeekDayDto>();
        var expected = string.Empty;

        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        Assert.AreEqual(expected, actual: format);
    }

    [TestMethod]
    public void GetWeekDaysFormat_WhenWeekDaysIsOne_ShouldReturnNewFormat()
    {
        var weekDays = new List<WeekDayDto>() 
        {
            new() { WeekDayId = (int)DayOfWeek.Monday, WeekDayName = WeekDaysName.Monday }
        };
        var expected = WeekDaysName.Monday;

        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        Assert.AreEqual(expected, actual: format);
    }

    [TestMethod]
    public void GetWeekDaysFormat_WhenWeekDaysIsTwo_ShouldReturnNewFormat()
    {
        var weekDays = new List<WeekDayDto>()
        {
            new() { WeekDayId = (int)DayOfWeek.Monday,    WeekDayName = WeekDaysName.Monday },
            new() { WeekDayId = (int)DayOfWeek.Wednesday, WeekDayName = WeekDaysName.Wednesday }
        };
        var expected = $"{WeekDaysName.Monday} y {WeekDaysName.Wednesday}";

        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        Assert.AreEqual(expected, actual: format);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetDataConsecutive), DynamicDataSourceType.Method)]
    public void GetWeekDaysFormat_WhenDaysAreConsecutive_ShouldReturnNewFormat(string testId, List<WeekDayDto> weekDays, string expected)
    {
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        Assert.AreEqual(expected, actual: format);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetDataNotConsecutive), DynamicDataSourceType.Method)]
    public void GetWeekDaysFormat_WhenDaysAreNotConsecutive_ShouldReturnNewFormat(string testId, List<WeekDayDto> weekDays, string expected)
    {
        var format = WeekDayFormat.GetWeekDaysFormat(weekDays);

        Assert.AreEqual(expected, actual: format);
    }
}
