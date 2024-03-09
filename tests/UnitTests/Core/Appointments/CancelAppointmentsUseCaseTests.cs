namespace UnitTests.Core.Appointments;

public class CancelAppointmentsUseCaseTests
{
    private IDateTimeService _dateTimeService;
    private ICurrentEmployee _currentEmployee;
    private CancelAppointmentsUseCase _cancelAppointmentsUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _dateTimeService           = Mock.Create<IDateTimeService>();
        _currentEmployee           = Mock.Create<ICurrentEmployee>();
        _cancelAppointmentsUseCase = new CancelAppointmentsUseCase(
            new AppSettings(),
            Mock.Create<IAppointmentRepository>(),
            Mock.Create<IInstantMessaging>(),
            _dateTimeService,
            _currentEmployee,
            new CancelAppointmentsValidator());
    }

    [Test]
    public async Task ExecuteAsync_WhenAllAppointmentsCanBeCancelled_ShouldReturnsResultWithoutAppointmentsId()
    {
        // Arrange
        var request = new CancelAppointmentsRequest
        {
            Reason = "Reason",
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new() 
                { 
                    AppointmentId = 1, 
                    PatientName = "Bob", 
                    PatientCellPhone = "3053581032", 
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(14, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 2,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(15, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 3,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(13, 0, 0) 
                }
            }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Mock.Arrange(() => _currentEmployee.IsOnlyDentist()).Returns(false);

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().Be(Messages.SuccessfullyCancelledAppointments);
        result.Data.Should().BeNull();
    }

    [Test]
    public async Task ExecuteAsync_WhenSomeAppointmentsCannotBeCancelled_ShouldReturnsResultWithAppointmentsId()
    {
        // Arrange
        int[] expectedAppointmentsId = { 4, 5 };
        int pastAppointments = 2;
        var expectedMessage = new AppointmentThatHasAlreadyPassedEmployeeError(pastAppointments).Message;
        var request = new CancelAppointmentsRequest
        {
            Reason = "Reason",
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new() 
                { 
                    AppointmentId = 1,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(14, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 2,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(15, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 3,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(08, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 4,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 01), 
                    StartHour = new TimeSpan(09, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 5,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 01), 
                    StartHour = new TimeSpan(13, 0, 0) 
                }
            }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 01, 20, 0, 0));
        Mock.Arrange(() => _currentEmployee.IsOnlyDentist()).Returns(false);

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message
            .Should()
            .Be(expectedMessage);

        result.Data
            .AppointmentsId
            .Should()
            .BeEquivalentTo(expectedAppointmentsId);
    }

    [Test]
    public async Task ExecuteAsync_WhenAppointmentsCannotBeCancelled_ShouldReturnsResultWithAppointmentsId()
    {
        // Arrange
        int[] expectedAppointmentsId = { 1, 2, 3, 4, 5 };
        int pastAppointments = 5;
        var expectedMessage = new AppointmentThatHasAlreadyPassedEmployeeError(pastAppointments).Message;
        var request = new CancelAppointmentsRequest
        {
            Reason = "Reason",
            Appointments = new List<CancelAppointmentsRequest.Appointment>
            {
                new() 
                { 
                    AppointmentId = 1,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(14, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 2,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(15, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 3,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 02), 
                    StartHour = new TimeSpan(08, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 4,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 01), 
                    StartHour = new TimeSpan(09, 0, 0) 
                },
                new() 
                { 
                    AppointmentId = 5,
                    PatientName = "Bob",
                    PatientCellPhone = "3053581032",
                    AppointmentDate = new DateTime(2022, 08, 01), 
                    StartHour = new TimeSpan(13, 0, 0) 
                }
            }
        };
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 02, 20, 0, 0));
        Mock.Arrange(() => _currentEmployee.IsOnlyDentist()).Returns(false);

        // Act
        var result = await _cancelAppointmentsUseCase.ExecuteAsync(request);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message
            .Should()
            .Be(expectedMessage);

        result.Data
            .AppointmentsId
            .Should()
            .BeEquivalentTo(expectedAppointmentsId);
    }
}
