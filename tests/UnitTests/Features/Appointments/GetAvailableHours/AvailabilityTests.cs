namespace DentallApp.UnitTests.Features.Appointments.GetAvailableHours;

public class AvailabilityTests
{
    [TestCaseSource(typeof(CalculateAvailableHoursTestCases))]
    public void CalculateAvailableHours_WhenNumberOfUnavailableHoursIsGreaterThanOrEqualToZero_ShouldReturnsAvailableHours(
        AvailabilityOptions options, 
        List<AvailableTimeRangeResponse> expectedList)
    {
        // Act
        var availableHours = Availability.CalculateAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public void CalculateAvailableHours_WhenDentistHasSomeTimeoffOrRestTime_ShouldTakeItAsRangeOfUnavailableTime()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeResponse>
        {
            new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("9:00") },
            new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("11:00") },
            new() { StartHour = TimeSpan.Parse("13:00"), EndHour = TimeSpan.Parse("14:00") },
            new() { StartHour = TimeSpan.Parse("14:30"), EndHour = TimeSpan.Parse("15:00") },
            new() { StartHour = TimeSpan.Parse("17:00"), EndHour = TimeSpan.Parse("17:30") }
        };
        var expectedList = new List<AvailableTimeRangeResponse>
        {
            new() { StartHour = "07:00", EndHour = "08:00" },
            new() { StartHour = "09:00", EndHour = "10:00" },
            new() { StartHour = "11:00", EndHour = "12:00" },
            new() { StartHour = "15:00", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "17:00" }
        };
        var timeOff = new UnavailableTimeRangeResponse
        {
            StartHour = TimeSpan.Parse("12:00"),
            EndHour   = TimeSpan.Parse("13:00")
        };
        unavailables.Add(timeOff);
        var options = new AvailabilityOptions
        {
            DentistStartHour = TimeSpan.Parse("7:00"),
            DentistEndHour   = TimeSpan.Parse("18:00"),
            ServiceDuration  = TimeSpan.FromMinutes(60),
            Unavailables     = unavailables
                .OrderBy(timeRange => timeRange.StartHour)
                .ThenBy(timeRange => timeRange.EndHour)
                .ToList()
        };

        // Act
        var availableHours = Availability.CalculateAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public void CalculateAvailableHours_WhenDurationOfDentalServiceIsEqualToZero_ShouldThrowArgumentException()
    {
        // Arrange
        var options = new AvailabilityOptions
        {
            Unavailables     = new List<UnavailableTimeRangeResponse>(),
            DentistStartHour = TimeSpan.Parse("9:30"),
            DentistEndHour   = TimeSpan.Parse("12:30"),
            ServiceDuration  = TimeSpan.FromMinutes(0)
        };

        // Act
        Action act = () => Availability.CalculateAvailableHours(options);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void CalculateAvailableHours_WhenNumberOfAvailableHoursIsZero_ShouldReturnsNull()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeResponse>
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
        var availableHours = Availability.CalculateAvailableHours(options);

        // Assert
        availableHours.Should().BeNull();
    }

    [Test]
    public void CalculateAvailableHours_WhenAppointmentDateIsEqualToTheCurrentDate_ShouldDiscardAvailableHoursThatAreLessThanTheCurrentTime()
    {
        // Arrange
        var unavailables = new List<UnavailableTimeRangeResponse>
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
        var expectedList = new List<AvailableTimeRangeResponse>
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
        var availableHours = Availability.CalculateAvailableHours(options);

        // Assert
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [TestCase("08:20", "09:00", 
              "09:00", "09:30")]
    [TestCase("11:40", "12:20",
              "11:00", "11:40")]
    [TestCase("10:00", "10:30",
              "10:40", "11:20")]
    [TestCase("10:00", "10:30",
              "09:00", "09:40")]
    public void IsNotAvailable_WhenNewTimeRangeIsAvailable_ShouldReturnsFalse(
        string newStartHour,
        string newEndHour, 
        string startHourNotAvailable,
        string startEndNotAvailable)
    {
        // Arrange
        var unavailableTimeRange = new UnavailableTimeRangeResponse
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

    [TestCase("09:00", "09:30",
              "09:00", "09:30")]
    [TestCase("09:00", "09:30",
              "09:20", "09:35")]
    [TestCase("10:00", "10:30",
              "10:25", "11:20")]
    [TestCase("10:00", "10:30",
              "09:00", "10:10")]
    [TestCase("10:00", "10:30",
              "10:29", "12:00")]
    [TestCase("12:20", "12:50",
              "12:00", "13:00")]
    public void IsNotAvailable_WhenNewTimeRangeIsNotAvailable_ShouldReturnsTrue(
        string newStartHour,
        string newEndHour,
        string startHourNotAvailable,
        string startEndNotAvailable)
    {
        // Arrange
        var unavailableTimeRange = new UnavailableTimeRangeResponse
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
