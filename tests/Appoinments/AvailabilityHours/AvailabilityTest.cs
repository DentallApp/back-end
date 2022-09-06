namespace DentallApp.Tests.Appoinments.AvailabilityHours;

[TestClass]
public partial class AvailabilityTest
{
    [DataTestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void GetAvailableHours_WhenNumberOfUnavailableHoursIsGreaterThanOrEqualToZero_ShouldReturnAvailableHours(
        string testId, AvailabilityOptions options, List<AvailableTimeRange> expected)
    {
        var availableHours = Availability.GetAvailableHours(options);

        Assert.AreEqual(expected.Count, actual: availableHours.Count);
        for(int i = 0; i < availableHours.Count; i++)
        {
            Assert.AreEqual(expected[i].StartHour, actual: availableHours[i].StartHour);
            Assert.AreEqual(expected[i].EndHour,   actual: availableHours[i].EndHour);
        }
    }

    [TestMethod]
    public void GetAvailableHours_WhenDurationOfDentalServiceIsEqualToZero_ShouldThrowArgumentException()
    {
        var options = new AvailabilityOptions
        {
            Unavailables     = new List<UnavailableTimeRange>(),
            DentistStartHour = TimeSpan.Parse("9:30"),
            DentistEndHour   = TimeSpan.Parse("12:30"),
            ServiceDuration  = TimeSpan.FromMinutes(0)
        };

        void action() => Availability.GetAvailableHours(options);

        Assert.ThrowsException<InvalidOperationException>(action);
    }

    [TestMethod]
    public void GetAvailableHours_WhenNumberOfAvailableHoursIsZero_ShouldReturnNull()
    {
        var unavailables = new List<UnavailableTimeRange>
        {
            new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("9:00") },
            new() { StartHour = TimeSpan.Parse("9:00"),  EndHour = TimeSpan.Parse("10:00") },
            new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("11:00") },
            new() { StartHour = TimeSpan.Parse("11:00"), EndHour = TimeSpan.Parse("12:00") }
        };
        var options = new AvailabilityOptions
        {
            Unavailables     = unavailables,
            DentistStartHour = TimeSpan.Parse("8:00"),
            DentistEndHour   = TimeSpan.Parse("12:00"),
            ServiceDuration  = TimeSpan.FromMinutes(60)
        };

        var availableHours = Availability.GetAvailableHours(options);

        Assert.IsNull(availableHours);
    }
}
