namespace DentallApp.UnitTests.Features.AppointmentCancellation;

[TestClass]
public class AppointmentCancellationServiceTests
{
    private IAppointmentCancellationRepository _appointmentRepository;
    private IDateTimeProvider _dateTimeProvider;
    private AppointmentCancellationService _appointmentService;

    [TestInitialize]
    public void TestInitialize()
    {
        _appointmentRepository = Mock.Create<IAppointmentCancellationRepository>();
        _dateTimeProvider      = Mock.Create<IDateTimeProvider>();
        _appointmentService    = new AppointmentCancellationService(_appointmentRepository,
                                                                    Mock.Create<IInstantMessaging>(),
                                                                    _dateTimeProvider); 
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenAllAppointmentsCanBeCancelled_ShouldReturnAnResponseWithoutAppointmentsId()
    {
        // Arrange
        var dto = new AppointmentCancelDto
        {
            Appointments = new List<AppointmentCancelDetailsDto>
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
        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        // Asserts
        response.Success.Should().BeTrue();
        response.Message.Should().Be(SuccessfullyCancelledAppointmentsMessage);
        response.Data.Should().BeNull();
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
        // Arrange
        var dto = new AppointmentCancelDto
        {
            Appointments = new List<AppointmentCancelDetailsDto>
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
        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message
                .Should()
                .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 2));

        response.Data.AppointmentsId
                     .Should()
                     .BeEquivalentTo(new[] { 4, 5 });
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
        // Arrange
        var dto = new AppointmentCancelDto
        {
            Appointments = new List<AppointmentCancelDetailsDto>
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
        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message
                .Should()
                .Be(string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 5));
            
        response.Data.AppointmentsId
                     .Should()
                     .BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
    }

    [TestMethod]
    public async Task CancelBasicUserAppointmentAsync_WhenAppointmentCannotBeCancelled_ShouldReturnFailureResponse()
    {
        // Arrange
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
        Mock.Arrange(() => _appointmentRepository.GetByIdAsync(Arg.AnyInt))
            .ReturnsAsync((int id) => new Appointment
            {
                Date      = new DateTime(2022, 08, 04),
                StartHour = new TimeSpan(13, 0, 0)
            });

        // Act
        var response = await _appointmentService.CancelBasicUserAppointmentAsync(default, default);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message.Should().Be(AppointmentThatHasAlreadyPassedBasicUserMessage);
    }
}
