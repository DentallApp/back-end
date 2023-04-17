namespace DentallApp.UnitTests.Features.AvailabilityHours;

[TestClass]
public partial class AvailabilityTests
{
    [DataTestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void GetAvailableHours_WhenNumberOfUnavailableHoursIsGreaterThanOrEqualToZero_ShouldReturnsAvailableHours(
        string testId, AvailabilityOptions options, List<AvailableTimeRangeDto> expectedList)
    {
        // Act
        var availableHours = Availability.GetAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [TestMethod]
    public void GetAvailableHours_WhenDentistHasSomeTimeoffOrRestTime_ShouldTakeItAsRangeOfUnavailableTime()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeDto>
        {
            new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("9:00") },
            new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("11:00") },
            new() { StartHour = TimeSpan.Parse("13:00"), EndHour = TimeSpan.Parse("14:00") },
            new() { StartHour = TimeSpan.Parse("14:30"), EndHour = TimeSpan.Parse("15:00") },
            new() { StartHour = TimeSpan.Parse("17:00"), EndHour = TimeSpan.Parse("17:30") }
        };
        var expectedList = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "07:00", EndHour = "08:00" },
            new() { StartHour = "09:00", EndHour = "10:00" },
            new() { StartHour = "11:00", EndHour = "12:00" },
            new() { StartHour = "15:00", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "17:00" }
        };
        var options = new AvailabilityOptions
        {
            DentistStartHour = TimeSpan.Parse("7:00"),
            DentistEndHour   = TimeSpan.Parse("18:00"),
            ServiceDuration  = TimeSpan.FromMinutes(60)
        };
        var timeOff = new UnavailableTimeRangeDto
        {
            StartHour = TimeSpan.Parse("12:00"),
            EndHour   = TimeSpan.Parse("13:00")
        };
        unavailables.Add(timeOff);
        options.Unavailables = unavailables.OrderBy(timeRange => timeRange.StartHour)
                                           .ThenBy(timeRange => timeRange.EndHour).ToList();

        // Act
        var availableHours = Availability.GetAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [TestMethod]
    public void GetAvailableHours_WhenDurationOfDentalServiceIsEqualToZero_ShouldThrowArgumentException()
    {
        // Arrange
        var options = new AvailabilityOptions
        {
            Unavailables     = new List<UnavailableTimeRangeDto>(),
            DentistStartHour = TimeSpan.Parse("9:30"),
            DentistEndHour   = TimeSpan.Parse("12:30"),
            ServiceDuration  = TimeSpan.FromMinutes(0)
        };

        // Act
        Action act = () => Availability.GetAvailableHours(options);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void GetAvailableHours_WhenNumberOfAvailableHoursIsZero_ShouldReturnsNull()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeDto>
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

        // Act
        var availableHours = Availability.GetAvailableHours(options);

        // Assert
        availableHours.Should().BeNull();
    }

    [TestMethod]
    public void GetAvailableHours_WhenAppointmentDateIsEqualToTheCurrentDate_ShouldDiscardAvailableHoursThatAreLessThanTheCurrentTime()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeDto>
        {
            new() { StartHour = TimeSpan.Parse("10:00"),  EndHour = TimeSpan.Parse("10:30") },
            new() { StartHour = TimeSpan.Parse("11:00"),  EndHour = TimeSpan.Parse("12:00") }
        };
        var options = new AvailabilityOptions
        {
            Unavailables       = unavailables,
            DentistStartHour   = TimeSpan.Parse("7:00"),
            DentistEndHour     = TimeSpan.Parse("17:00"),
            ServiceDuration    = TimeSpan.FromMinutes(30),
            AppointmentDate    = new DateTime(2022, 08, 01, 0, 0, 0),
            CurrentTimeAndDate = new DateTime(2022, 08, 01, 12, 0, 0)
        };
        var expectedList = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "12:30", EndHour = "13:00" },
            new() { StartHour = "13:00", EndHour = "13:30" },
            new() { StartHour = "13:30", EndHour = "14:00" },
            new() { StartHour = "14:00", EndHour = "14:30" },
            new() { StartHour = "14:30", EndHour = "15:00" },
            new() { StartHour = "15:00", EndHour = "15:30" },
            new() { StartHour = "15:30", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "16:30" },
            new() { StartHour = "16:30", EndHour = "17:00" }
        };

        // Act
        var availableHours = Availability.GetAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [DataTestMethod]
    [DataRow("08:20", "09:00", 
             "09:00", "09:30")]
    [DataRow("11:40", "12:20",
             "11:00", "11:40")]
    [DataRow("10:00", "10:30",
             "10:40", "11:20")]
    [DataRow("10:00", "10:30",
             "09:00", "09:40")]
    public void IsNotAvailable_WhenNewTimeRangeIsAvailable_ShouldReturnsFalse(
        string newStartHour,
        string newEndHour, 
        string startHourNotAvailable,
        string startEndNotAvailable)
    {
        // Arrange
        var unavailableTimeRange = new UnavailableTimeRangeDto
        {
            StartHour = TimeSpan.Parse(startHourNotAvailable),
            EndHour   = TimeSpan.Parse(startEndNotAvailable)
        };
        var newStartHourSpan = TimeSpan.Parse(newStartHour);
        var newEndHourSpan   = TimeSpan.Parse(newEndHour);

        // Act
        bool result = Availability.IsNotAvailable(ref newStartHourSpan, ref newEndHourSpan, unavailableTimeRange);

        // Assert
        result.Should().BeFalse();
    }

    [DataTestMethod]
    [DataRow("09:00", "09:30",
             "09:00", "09:30")]
    [DataRow("09:00", "09:30",
             "09:20", "09:35")]
    [DataRow("10:00", "10:30",
             "10:25", "11:20")]
    [DataRow("10:00", "10:30",
             "09:00", "10:10")]
    [DataRow("10:00", "10:30",
             "10:29", "12:00")]
    [DataRow("12:20", "12:50",
             "12:00", "13:00")]
    public void IsNotAvailable_WhenNewTimeRangeIsNotAvailable_ShouldReturnsTrue(
        string newStartHour,
        string newEndHour,
        string startHourNotAvailable,
        string startEndNotAvailable)
    {
        // Arrange
        var unavailableTimeRange = new UnavailableTimeRangeDto
        {
            StartHour = TimeSpan.Parse(startHourNotAvailable),
            EndHour   = TimeSpan.Parse(startEndNotAvailable)
        };
        var newStartHourSpan = TimeSpan.Parse(newStartHour);
        var newEndHourSpan   = TimeSpan.Parse(newEndHour);

        // Act
        bool result = Availability.IsNotAvailable(ref newStartHourSpan, ref newEndHourSpan, unavailableTimeRange);

        // Assert
        result.Should().BeTrue();
    }
}
