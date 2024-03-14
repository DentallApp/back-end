namespace UnitTests.Shared.Entities.EmployeeSchedules;

public class IEmployeeScheduleExtensionsTests
{
    [Test]
    public void IsMorningSchedule_WhenScheduleIsMorning_ShouldReturnsTrue()
    {
        // Arrange
        var employeeSchedule = new EmployeeScheduleFake
        {
            MorningStartHour = new TimeSpan(7, 0, 0),
            MorningEndHour   = new TimeSpan(12, 0, 0)
        };

        // Act
        bool actual = employeeSchedule.IsMorningSchedule();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsAfternoonSchedule_WhenScheduleIsAfternoon_ShouldReturnsTrue()
    {
        // Arrange
        var employeeSchedule = new EmployeeScheduleFake
        {
            AfternoonStartHour = new TimeSpan(13, 0, 0),
            AfternoonEndHour   = new TimeSpan(18, 0, 0)
        };

        // Act
        bool actual = employeeSchedule.IsAfternoonSchedule();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasNotSchedule_WhenThereAreNoSchedules_ShouldReturnsTrue()
    {
        // Arrange
        var employeeSchedule = new EmployeeScheduleFake();

        // Act
        bool actual = employeeSchedule.HasNotSchedule();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasFullSchedule_WhenHasFullSchedule_ShouldReturnsTrue()
    {
        // Arrange
        var employeeSchedule = new EmployeeScheduleFake
        {
            MorningStartHour   = new TimeSpan(7, 0, 0),
            MorningEndHour     = new TimeSpan(12, 0, 0),
            AfternoonStartHour = new TimeSpan(13, 0, 0),
            AfternoonEndHour   = new TimeSpan(18, 0, 0)
        };

        // Act
        bool actual = employeeSchedule.HasFullSchedule();

        // Assert
        actual.Should().BeTrue();
    }
}
