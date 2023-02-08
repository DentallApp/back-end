namespace DentallApp.UnitTests.Features.AvailabilityHours;

[TestClass]
public class AvailabilityServiceTests
{
    private IAppointmentRepository _appointmentRepository;
    private IEmployeeScheduleRepository _employeeScheduleRepository;
    private IGeneralTreatmentRepository _dentalServiceRepository;
    private IHolidayOfficeRepository _holidayOfficeRepository;
    private AvailabilityService _availabilityService;
    private IDateTimeProvider _dateTimeProvider;

    [TestInitialize]
    public void TestInitialize()
    {
        _appointmentRepository      = Mock.Create<IAppointmentRepository>();
        _employeeScheduleRepository = Mock.Create<IEmployeeScheduleRepository>();
        _dentalServiceRepository    = Mock.Create<IGeneralTreatmentRepository>();
        _holidayOfficeRepository    = Mock.Create<IHolidayOfficeRepository>();
        _dateTimeProvider           = Mock.Create<IDateTimeProvider>();
        _availabilityService        = new AvailabilityService(_appointmentRepository,
                                                              _employeeScheduleRepository,
                                                              _dentalServiceRepository,
                                                              _holidayOfficeRepository,
                                                              _dateTimeProvider);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenAppointmentDateIsPublicHoliday_ShouldReturnAnErrorMessage()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _holidayOfficeRepository.IsPublicHolidayAsync(Arg.AnyInt, Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(true);

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected: AppointmentDateIsPublicHolidayMessage, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenDentistIsNotAvailableOnGivenDay_ShouldReturnAnErrorMessage()
    {
        var expected = string.Format(DentistNotAvailableMessage, WeekDaysType.WeekDays[0]);
        var availableTimeRangePostDto = new AvailableTimeRangePostDto { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .DoNothing();

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenEmployeeScheduleIsInactive_ShouldReturnAnErrorMessage()
    {
        var expected = string.Format(DentistNotAvailableMessage, WeekDaysType.WeekDays[0]);
        var availableTimeRangePostDto = new AvailableTimeRangePostDto { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto { IsEmployeeScheculeDeleted = true });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenOfficeScheduleIsInactive_ShouldReturnAnErrorMessage()
    {
        var expected = string.Format(OfficeClosedForSpecificDayMessage, WeekDaysType.WeekDays[0]);
        var availableTimeRangePostDto = new AvailableTimeRangePostDto { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto { IsOfficeScheduleDeleted = true });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenOfficeIsInactive_ShouldReturnAnErrorMessage()
    {
        var expected = string.Format(OfficeClosedForSpecificDayMessage, WeekDaysType.WeekDays[0]);
        var availableTimeRangePostDto = new AvailableTimeRangePostDto { AppointmentDate = new DateTime(2023, 01, 01) };
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto { IsOfficeDeleted = true });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenEmployeeDoesNotHaveMorningOrAfternoonSchedule_ShouldReturnAnErrorMessage()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto();
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto());

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected: NoMorningOrAfternoonHoursMessage, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenDentalServiceIdIsInvalid_ShouldReturnAnErrorMessage()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto();
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto
            { 
                MorningStartHour = new TimeSpan(8, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _dentalServiceRepository.GetTreatmentWithDurationAsync(Arg.AnyInt))
            .DoNothing();

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected: DentalServiceNotAvailableMessage, actual: response.Message);
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenEmployeeHasMorningAndAfternoonSchedule_ShouldReturnAvailableHours()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expected = new List<AvailableTimeRangeDto>
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
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto
            {
                MorningStartHour   = new TimeSpan(7, 0, 0),
                MorningEndHour     = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _appointmentRepository.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _dentalServiceRepository.GetTreatmentWithDurationAsync(Arg.AnyInt))
            .ReturnsAsync(new GeneralTreatmentGetDurationDto { Duration = 40 });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);
        var availableHours = response.Data.ToList();

        Assert.IsTrue(response.Success);
        Assert.AreEqual(expected.Count, actual: availableHours.Count);
        for (int i = 0; i < availableHours.Count; i++)
        {
            Assert.AreEqual(expected[i].StartHour, actual: availableHours[i].StartHour);
            Assert.AreEqual(expected[i].EndHour,   actual: availableHours[i].EndHour);
        }
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenEmployeeHasMorningSchedule_ShouldReturnAvailableHours()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expected = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "09:00", EndHour = "09:40" },
            new() { StartHour = "09:40", EndHour = "10:20" },
            new() { StartHour = "10:20", EndHour = "11:00" },
            new() { StartHour = "11:00", EndHour = "11:40" }
        };
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto
            {
                MorningStartHour = new TimeSpan(7, 0, 0),
                MorningEndHour   = new TimeSpan(12, 0, 0)
            });
        Mock.Arrange(() => _appointmentRepository.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") }
            });
        Mock.Arrange(() => _dentalServiceRepository.GetTreatmentWithDurationAsync(Arg.AnyInt))
            .ReturnsAsync(new GeneralTreatmentGetDurationDto { Duration = 40 });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);
        var availableHours = response.Data.ToList();

        Assert.IsTrue(response.Success);
        Assert.AreEqual(expected.Count, actual: availableHours.Count);
        for (int i = 0; i < availableHours.Count; i++)
        {
            Assert.AreEqual(expected[i].StartHour, actual: availableHours[i].StartHour);
            Assert.AreEqual(expected[i].EndHour,   actual: availableHours[i].EndHour);
        }
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenEmployeeHasAfternoonSchedule_ShouldReturnAvailableHours()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto() { AppointmentDate = new DateTime(2022, 05, 10) };
        var expected = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "13:00", EndHour = "13:40" },
            new() { StartHour = "14:40", EndHour = "15:20" },
            new() { StartHour = "15:20", EndHour = "16:00" },
            new() { StartHour = "16:00", EndHour = "16:40" }
        };
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _appointmentRepository.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") }
            });
        Mock.Arrange(() => _dentalServiceRepository.GetTreatmentWithDurationAsync(Arg.AnyInt))
            .ReturnsAsync(new GeneralTreatmentGetDurationDto { Duration = 40 });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);
        var availableHours = response.Data.ToList();

        Assert.IsTrue(response.Success);
        Assert.AreEqual(expected.Count, actual: availableHours.Count);
        for (int i = 0; i < availableHours.Count; i++)
        {
            Assert.AreEqual(expected[i].StartHour, actual: availableHours[i].StartHour);
            Assert.AreEqual(expected[i].EndHour,   actual: availableHours[i].EndHour);
        }
    }

    [TestMethod]
    public async Task GetAvailableHoursAsync_WhenSchedulesAreNotAvailable_ShouldReturnResponseWithoutAvailableHours()
    {
        var availableTimeRangePostDto = new AvailableTimeRangePostDto() { AppointmentDate = new DateTime(2022, 05, 10) };
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 05, 01, 8, 30, 0));
        Mock.Arrange(() => _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(new EmployeeScheduleByWeekDayDto
            {
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour   = new TimeSpan(17, 0, 0)
            });
        Mock.Arrange(() => _appointmentRepository.GetUnavailableHoursAsync(Arg.AnyInt, Arg.AnyDateTime))
            .ReturnsAsync(new List<UnavailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("13:00"), EndHour = TimeSpan.Parse("13:40") },
                new() { StartHour = TimeSpan.Parse("14:00"), EndHour = TimeSpan.Parse("14:40") },
                new() { StartHour = TimeSpan.Parse("14:40"), EndHour = TimeSpan.Parse("15:20") },
                new() { StartHour = TimeSpan.Parse("15:20"), EndHour = TimeSpan.Parse("16:00") },
                new() { StartHour = TimeSpan.Parse("16:00"), EndHour = TimeSpan.Parse("16:40") }
            });
        Mock.Arrange(() => _dentalServiceRepository.GetTreatmentWithDurationAsync(Arg.AnyInt))
            .ReturnsAsync(new GeneralTreatmentGetDurationDto { Duration = 40 });

        var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);

        Assert.IsFalse(response.Success);
        Assert.IsNull(response.Data);
        Assert.AreEqual(expected: NoSchedulesAvailableMessage, actual: response.Message);
    }
}


