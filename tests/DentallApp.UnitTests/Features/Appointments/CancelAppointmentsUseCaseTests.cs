namespace DentallApp.UnitTests.Features.Appointments;

public class CancelAppointmentsUseCaseTests
{
    private IDateTimeProvider _dateTimeProvider;
    private CancelAppointmentsUseCase _cancelAppointmentsUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _dateTimeProvider          = Mock.Create<IDateTimeProvider>();
        _cancelAppointmentsUseCase = new CancelAppointmentsUseCase(
            Mock.Create<IAppointmentRepository>(),
            Mock.Create<IInstantMessaging>(),
            _dateTimeProvider);
    }

    [Test]
    public async Task Execute_WhenAllAppointmentsCanBeCancelled_ShouldReturnsAnResponseWithoutAppointmentsId()
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
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var response = await _cancelAppointmentsUseCase.Execute(claimsPrincipal, request);

        // Asserts
        response.Success.Should().BeTrue();
        response.Message.Should().Be(SuccessfullyCancelledAppointmentsMessage);
        response.Data.Should().BeNull();
    }

    [Test]
    public async Task Execute_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnsAnResponseWithAppointmentsId()
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
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var response = await _cancelAppointmentsUseCase.Execute(claimsPrincipal, request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message
                .Should()
                .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 2));

        response.Data.AppointmentsId
                     .Should()
                     .BeEquivalentTo(new[] { 4, 5 });
    }

    [Test]
    public async Task Execute_WhenAppointmentsCannotBeCancelled_ShouldReturnsAnResponseWithAppointmentsId()
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
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 02, 20, 0, 0));
        Environment.SetEnvironmentVariable(AppSettings.BusinessName, " ");

        // Act
        var response = await _cancelAppointmentsUseCase.Execute(claimsPrincipal, request);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message
                .Should()
                .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 5));

        response.Data
                .AppointmentsId
                .Should()
                .BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
    }
}
