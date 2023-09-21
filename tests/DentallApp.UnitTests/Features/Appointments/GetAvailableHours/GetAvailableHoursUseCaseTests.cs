namespace DentallApp.UnitTests.Features.Appointments.GetAvailableHours;

public class GetAvailableHoursUseCaseTests
{
    private IGetUnavailableHoursUseCase _getUnavailableHoursUseCase;
    private IEmployeeScheduleRepository _employeeScheduleRepository;
    private IGeneralTreatmentRepository _treatmentRepository;
    private IOfficeHolidayRepository _officeHolidayRepository;
    private GetAvailableHoursUseCase _getAvailableHoursUseCase;
    private IDateTimeService _dateTimeService;

    [SetUp]
    public void TestInitialize()
    {
        _getUnavailableHoursUseCase = Mock.Create<IGetUnavailableHoursUseCase>();
        _employeeScheduleRepository = Mock.Create<IEmployeeScheduleRepository>();
        _treatmentRepository        = Mock.Create<IGeneralTreatmentRepository>();
        _officeHolidayRepository    = Mock.Create<IOfficeHolidayRepository>();
        _dateTimeService            = Mock.Create<IDateTimeService>();
        _getAvailableHoursUseCase   = new GetAvailableHoursUseCase(
            _getUnavailableHoursUseCase,
            _employeeScheduleRepository,
            _treatmentRepository,
            _officeHolidayRepository,
            _dateTimeService);
    }

    [Test]
    public async Task Execute_WhenAppointmentDateIsPublicHoliday_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _officeHolidayRepository.IsPublicHoliday(Arg.AnyInt, Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(true);

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(AppointmentDateIsPublicHolidayMessage);
    }

    [Test]
    public async Task Execute_WhenDentistIsNotAvailableOnGivenDay_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(DentistNotAvailableMessage, WeekDaysType.WeekDays[0]);
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .DoNothing();

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task Execute_WhenEmployeeScheduleIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(DentistNotAvailableMessage, WeekDaysType.WeekDays[0]);
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsEmployeeScheculeDeleted = true });

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task Execute_WhenOfficeScheduleIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(OfficeClosedForSpecificDayMessage, WeekDaysType.WeekDays[0]);
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsOfficeScheduleDeleted = true });

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(expectedMessage);
    }

    [Test]
    public async Task Execute_WhenOfficeIsInactive_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var expectedMessage = string.Format(OfficeClosedForSpecificDayMessage, WeekDaysType.WeekDays[0]);
        var request = new AvailableTimeRangeRequest { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse { IsOfficeDeleted = true });

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(expectedMessage);;
    }

    [Test]
    public async Task Execute_WhenEmployeeDoesNotHaveMorningOrAfternoonSchedule_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest();
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse());

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(NoMorningOrAfternoonHoursMessage);
    }

    [Test]
    public async Task Execute_WhenDentalServiceIdIsInvalid_ShouldReturnsAnErrorMessage()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest();
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            { 
                MorningStartHour = new TimeSpan(8, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _treatmentRepository.GetDuration(Arg.AnyInt))
            .DoNothing();

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(DentalServiceNotAvailableMessage);
    }

    [Test]
    public async Task Execute_WhenEmployeeHasMorningAndAfternoonSchedule_ShouldReturnsAvailableHours()
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
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                MorningStartHour   = new TimeSpan(7, 0, 0),
                MorningEndHour     = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _getUnavailableHoursUseCase.Execute(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _treatmentRepository.GetDuration(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);
        var availableHours = response.Data.ToList();

        // Asserts
        response.Success.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task Execute_WhenEmployeeHasMorningSchedule_ShouldReturnsAvailableHours()
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
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                MorningStartHour = new TimeSpan(7, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _getUnavailableHoursUseCase.Execute(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") }
            });
        Mock.Arrange(() => _treatmentRepository.GetDuration(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);
        var availableHours = response.Data.ToList();

        // Asserts
        response.Success.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task Execute_WhenEmployeeHasAfternoonSchedule_ShouldReturnsAvailableHours()
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
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _getUnavailableHoursUseCase.Execute(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _treatmentRepository.GetDuration(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);
        var availableHours = response.Data.ToList();

        // Asserts
        response.Success.Should().BeTrue();
        availableHours.Should().BeEquivalentTo(expectedList);
    }

    [Test]
    public async Task Execute_WhenSchedulesAreNotAvailable_ShouldReturnsResponseWithoutAvailableHours()
    {
        // Arrange
        var request = new AvailableTimeRangeRequest() { AppointmentDate = new DateTime(2022, 05, 10) };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _employeeScheduleRepository.GetByWeekDayId(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleResponse
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _getUnavailableHoursUseCase.Execute(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeResponse>
            {
                new() { StartHour = TimeSpan.Parse("13:00"), EndHour = TimeSpan.Parse("13:40") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") },
                new() { StartHour = TimeSpan.Parse("14:40"), EndHour = TimeSpan.Parse("15:20") },
                new() { StartHour = TimeSpan.Parse("15:20"), EndHour = TimeSpan.Parse("16:00") },
                new() { StartHour = TimeSpan.Parse("16:00"), EndHour = TimeSpan.Parse("16:40") }
            });
        Mock.Arrange(() => _treatmentRepository.GetDuration(Arg.AnyInt))
            .ReturnsAsync(40);

        // Act
        var response = await _getAvailableHoursUseCase.Execute(request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be(NoSchedulesAvailableMessage);
    }
}


