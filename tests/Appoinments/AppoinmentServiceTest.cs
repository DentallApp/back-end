namespace DentallApp.Tests.Appoinments;

[TestClass]
public class AppoinmentServiceTest
{
    private IAppoinmentRepository _appoinmentRepository;
    private IDateTimeProvider _dateTimeProvider;
    private IAppoinmentService _service;

    [TestInitialize]
    public void TestInitialize()
    {
        _appoinmentRepository = Mock.Create<IAppoinmentRepository>();
        _dateTimeProvider     = Mock.Create<IDateTimeProvider>();
        _service              = new AppoinmentService(_appoinmentRepository,
                                                      Mock.Create<IInstantMessaging>(), 
                                                      Mock.Create<ISpecificTreatmentRepository>(),
                                                      _dateTimeProvider); 
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenAllAppointmentsCanBeCancelled_ShouldReturnAnResponseWithoutAppointmentsId()
    {
        var dto = new AppoinmentCancelDto
        {
            Appoinments = new List<AppoinmentCancelDetailsDto>
            {
                new () { AppoinmentId = 1, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppoinmentId = 2, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppoinmentId = 3, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(13, 0, 0) }
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

        var response = await _service.CancelAppointmentsAsync(claimsPrincipal, dto);

        Assert.IsTrue(response.Success);
        Assert.AreEqual(expected: SuccessfullyCancelledAppointmentsMessage, actual: response.Message);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
        var dto = new AppoinmentCancelDto
        {
            Appoinments = new List<AppoinmentCancelDetailsDto>
            {
                new () { AppoinmentId = 1, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppoinmentId = 2, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppoinmentId = 3, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(08, 0, 0) },
                new () { AppoinmentId = 4, AppoinmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(09, 0, 0) },
                new () { AppoinmentId = 5, AppoinmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(13, 0, 0) }
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

        var response = await _service.CancelAppointmentsAsync(claimsPrincipal, dto);

        var message = string.Format(AppoinmentThatHasAlreadyPassedEmployeeMessage, 2);
        var appoinmentsId = response.Data.AppoinmentsId.ToList();
        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: message, actual: response.Message);
        Assert.AreEqual(expected: 2, actual: appoinmentsId.Count);
        Assert.AreEqual(expected: 4, actual: appoinmentsId[0]);
        Assert.AreEqual(expected: 5, actual: appoinmentsId[1]);
    }

    [TestMethod]
    public async Task CancelAppointmentsAsync_WhenAppointmentsCannotBeCancelled_ShouldReturnAnResponseWithAppointmentsId()
    {
        var dto = new AppoinmentCancelDto
        {
            Appoinments = new List<AppoinmentCancelDetailsDto>
            {
                new () { AppoinmentId = 1, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(14, 0, 0) },
                new () { AppoinmentId = 2, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(15, 0, 0) },
                new () { AppoinmentId = 3, AppoinmentDate = new DateTime(2022, 08, 02), StartHour = new TimeSpan(08, 0, 0) },
                new () { AppoinmentId = 4, AppoinmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(09, 0, 0) },
                new () { AppoinmentId = 5, AppoinmentDate = new DateTime(2022, 08, 01), StartHour = new TimeSpan(13, 0, 0) }
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

        var response = await _service.CancelAppointmentsAsync(claimsPrincipal, dto);

        var message = string.Format(AppoinmentThatHasAlreadyPassedEmployeeMessage, 5);
        var appoinmentsId = response.Data.AppoinmentsId.ToList();
        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: message, actual: response.Message);
        Assert.AreEqual(expected: 5, actual: appoinmentsId.Count);
        Assert.AreEqual(expected: 1, actual: appoinmentsId[0]);
        Assert.AreEqual(expected: 2, actual: appoinmentsId[1]);
        Assert.AreEqual(expected: 3, actual: appoinmentsId[2]);
        Assert.AreEqual(expected: 4, actual: appoinmentsId[3]);
        Assert.AreEqual(expected: 5, actual: appoinmentsId[4]);
    }

    [TestMethod]
    public async Task CancelBasicUserAppointmentAsync_WhenAppointmentCannotBeCancelled_ShouldReturnFailureResponse()
    {
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
        Mock.Arrange(() => _appoinmentRepository.GetByIdAsync(default))
            .ReturnsAsync((int id) => new Appoinment
            {
                Date      = new DateTime(2022, 08, 04),
                StartHour = new TimeSpan(13, 0, 0)
            });

        var response = await _service.CancelBasicUserAppointmentAsync(default, default);

        Assert.IsFalse(response.Success);
        Assert.AreEqual(expected: AppoinmentThatHasAlreadyPassedBasicUserMessage, actual: response.Message);
    }
}
