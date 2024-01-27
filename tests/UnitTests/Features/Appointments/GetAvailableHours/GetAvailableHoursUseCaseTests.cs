namespace DentallApp.UnitTests.Features.Appointments.GetAvailableHours;

public class GetAvailableHoursUseCaseTests
{
    private IAvailabilityQueries _queries;
    private IGetAvailableHoursUseCase _getAvailableHoursUseCase;
    private IDateTimeService _dateTimeService;

    [SetUp]
    public void TestInitialize()
    {
        _queries                  = Mock.Create<IAvailabilityQueries>();
        _dateTimeService          = Mock.Create<IDateTimeService>();
        _getAvailableHoursUseCase = new GetAvailableHoursUseCase(_queries, _dateTimeService);
    }

    [Test]
    public async Task ExecuteAsync_WhenAppointmentDateIsPublicHoliday_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _queries.IsPublicHolidayAsync(Arg.AnyInt, Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(true);

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(Messages.AppointmentDateIsPublicHoliday);
    }

    [Test]
    public async Task ExecuteAsync_WhenDentistIsNotAvailableOnGivenDay_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(
            Messages.DentistNotAvailable, 
            WeekdayCollection.GetName(DayOfWeek.Sunday));
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .DoNothing();

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task ExecuteAsync_WhenEmployeeScheduleIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(
            Messages.DentistNotAvailable, 
            WeekdayCollection.GetName(DayOfWeek.Sunday));
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsEmployeeScheculeDeleted = true });

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task ExecuteAsync_WhenOfficeScheduleIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(
            Messages.OfficeClosedForSpecificDay, 
            WeekdayCollection.GetName(DayOfWeek.Sunday));
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsOfficeScheduleDeleted = true });

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task ExecuteAsync_WhenOfficeIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(
            Messages.OfficeClosedForSpecificDay, 
            WeekdayCollection.GetName(DayOfWeek.Sunday));
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsOfficeDeleted = true });

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(expectedMessage);;
    }

    [Test]
    public async Task ExecuteAsync_WhenEmployeeDoesNotHaveMorningOrAfternoonSchedule_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest();
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse());

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(Messages.NoMorningOrAfternoonHours);
    }

    [Test]
    public async Task ExecuteAsync_WhenDentalServiceIdIsInvalid_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest();
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            { 
                MorningStartHour = new TimeSpan(8, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _queries.GetTreatmentDurationAsync(Arg.AnyInt))
            .DoNothing();

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(Messages.DentalServiceNotAvailable);
    }

    [Test]
    public async Task ExecuteAsync_WhenEmployeeHasMorningAndAfternoonSchedule_ShouldReturnsAvailableHours()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expectedList = new List<AvailableTimeRangeResponse>
        {
            new() { StartHour = "09:00", EndHour = "09:40" },
            new() { StartHour = "09:40", EndHour = "10:20" },
            new() { StartHour = "10:20", EndHour = "11:00" },
            new() { StartHour = "11:00", EndHour = "11:40" },
            new() { StartHour = "13:00", EndHour = "13:40" },
            new() { StartHour = "14:40", EndHour = "15:20" },
            new() { StartHour = "15:20", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "16:40" }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                MorningStartHour   = new TimeSpan(7, 0, 0),
                MorningEndHour     = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _queries.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _queries.GetTreatmentDurationAsync(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);
        var availableHours = result.Data.ToList();

        // Asserts
        result.IsSuccess.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task ExecuteAsync_WhenEmployeeHasMorningSchedule_ShouldReturnsAvailableHours()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expectedList = new List<AvailableTimeRangeResponse>
        {
            new() { StartHour = "09:00", EndHour = "09:40" },
            new() { StartHour = "09:40", EndHour = "10:20" },
            new() { StartHour = "10:20", EndHour = "11:00" },
            new() { StartHour = "11:00", EndHour = "11:40" }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                MorningStartHour = new TimeSpan(7, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _queries.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") }
            });
        Mock.Arrange(() => _queries.GetTreatmentDurationAsync(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);
        var availableHours = result.Data.ToList();

        // Asserts
        result.IsSuccess.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task ExecuteAsync_WhenEmployeeHasAfternoonSchedule_ShouldReturnsAvailableHours()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expectedList = new List<AvailableTimeRangeResponse>
        {
            new() { StartHour = "13:00", EndHour = "13:40" },
            new() { StartHour = "14:40", EndHour = "15:20" },
            new() { StartHour = "15:20", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "16:40" }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _queries.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _queries.GetTreatmentDurationAsync(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);
        var availableHours = result.Data.ToList();

        // Asserts
        result.IsSuccess.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task ExecuteAsync_WhenSchedulesAreNotAvailable_ShouldReturnsResultWithoutAvailableHours()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest() { AppointmentDate = new DateTime(2022, 05, 10) };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _queries.GetEmployeeScheduleAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _queries.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("13:00"), EndHour = TimeSpan.Parse("13:40") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") },
                new() { StartHour = TimeSpan.Parse("14:40"), EndHour = TimeSpan.Parse("15:20") },
                new() { StartHour = TimeSpan.Parse("15:20"), EndHour = TimeSpan.Parse("16:00") },
                new() { StartHour = TimeSpan.Parse("16:00"), EndHour = TimeSpan.Parse("16:40") }
            });
        Mock.Arrange(() => _queries.GetTreatmentDurationAsync(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var result = await _getAvailableHoursUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
        result.Message.Should().Be(Messages.NoSchedulesAvailable);
    }
}


