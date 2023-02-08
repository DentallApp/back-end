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

        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        Assert.IsTrue(response.Success);
        Assert.AreEqual(expected: SuccessfullyCancelledAppointmentsMessage, actual: response.Message);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
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

        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        var message = string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 2);
        var appointmentsId = response.Data.AppointmentsId.ToList();
        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: message, actual: response.Message);
        Assert.AreEqual(expected: 2, actual: appointmentsId.Count);
        Assert.AreEqual(expected: 4, actual: appointmentsId[0]);
        Assert.AreEqual(expected: 5, actual: appointmentsId[1]);
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
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

        var response = await _appointmentService.CancelAppointmentsAsync(claimsPrincipal, dto);

        var message = string.Format(AppointmentThatHasAlreadyPassedEmployeeMessage, 5);
        var appointmentsId = response.Data.AppointmentsId.ToList();
        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: message, actual: response.Message);
        Assert.AreEqual(expected: 5, actual: appointmentsId.Count);
        Assert.AreEqual(expected: 1, actual: appointmentsId[0]);
        Assert.AreEqual(expected: 2, actual: appointmentsId[1]);
        Assert.AreEqual(expected: 3, actual: appointmentsId[2]);
        Assert.AreEqual(expected: 4, actual: appointmentsId[3]);
        Assert.AreEqual(expected: 5, actual: appointmentsId[4]);
    }

    [TestMethod]
    public async Task CancelBasicUserAppointmentAsync_WhenAppointmentCannotBeCancelled_ShouldReturnFailureResponse()
    {
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
        Mock.Arrange(() => _appointmentRepository.GetByIdAsync(Arg.AnyInt))
            .ReturnsAsync((int id) => new Appointment
            {
                Date      = new DateTime(2022, 08, 04),
                StartHour = new TimeSpan(13, 0, 0)
            });

        var response = await _appointmentService.CancelBasicUserAppointmentAsync(default, default);

        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: AppointmentThatHasAlreadyPassedBasicUserMessage, actual: response.Message);
    }
}
