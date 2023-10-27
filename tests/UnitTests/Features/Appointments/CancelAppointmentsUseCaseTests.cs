namespace DentallApp.UnitTests.Features.Appointments;

public class CancelAppointmentsUseCaseTests
{
    private IDateTimeService _dateTimeService;
    private CancelAppointmentsUseCase _cancelAppointmentsUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _dateTimeService           = Mock.Create<IDateTimeService>();
        _cancelAppointmentsUseCase = new CancelAppointmentsUseCase(
            Mock.Create<IAppointmentRepository>(),
            Mock.Create<IInstantMessaging>(),
            _dateTimeService);
    }

    [Test]
    public async Task ExecuteAsync_WhenAllAppointmentsCanBeCancelled_ShouldReturnsResultWithoutAppointmentsId()
    {
        // Arrange
        var request = new CancelAppointmentsRequest
        {
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new () { AppointmentId = 1, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppointmentId = 2, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppointmentId = 3, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(13, 0, 0) }
            }
        };
        var claims = new Claim[]
        {
            new (CustomClaimsType.EmployeeId, "1"),
            new (ClaimTypes.Role, RolesName.Dentist)
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(claimsPrincipal, request);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().Be(SuccessfullyCancelledAppointmentsMessage);
        result.Data.Should().BeNull();
    }

    [Test]
    public async Task ExecuteAsync_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnsResultWithAppointmentsId()
    {
        // Arrange
        var request = new CancelAppointmentsRequest
        {
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new () { AppointmentId = 1, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppointmentId = 2, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppointmentId = 3, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(08, 0, 0) },
                new () { AppointmentId = 4, AppointmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(09, 0, 0) },
                new () { AppointmentId = 5, AppointmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(13, 0, 0) }
            }
        };
        var claims = new Claim[]
        {
            new (CustomClaimsType.EmployeeId, "1"),
            new (ClaimTypes.Role, RolesName.Dentist)
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(claimsPrincipal, request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message
            .Should()
            .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 2));

        result.Data
            .AppointmentsId
            .Should()
            .BeEquivalentTo(new[] { 4, 5 });
    }

    [Test]
    public async Task ExecuteAsync_WhenAppointmentsCannotBeCancelled_ShouldReturnsResultWithAppointmentsId()
    {
        // Arrange
        var request = new CancelAppointmentsRequest
        {
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new () { AppointmentId = 1, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppointmentId = 2, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppointmentId = 3, AppointmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(08, 0, 0) },
                new () { AppointmentId = 4, AppointmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(09, 0, 0) },
                new () { AppointmentId = 5, AppointmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(13, 0, 0) }
            }
        };
        var claims = new Claim[]
        {
            new (CustomClaimsType.EmployeeId, "1"),
            new (ClaimTypes.Role, RolesName.Dentist)
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 02, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(claimsPrincipal, request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message
            .Should()
            .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 5));

        result.Data
            .AppointmentsId
            .Should()
            .BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
    }
}
