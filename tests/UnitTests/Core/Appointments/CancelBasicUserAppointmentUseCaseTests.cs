namespace UnitTests.Core.Appointments;

public class CancelBasicUserAppointmentUseCaseTests
{
    private IRepository<Appointment> _repository;
    private IDateTimeService _dateTimeService;
    private ICurrentUser _currentUser;
    private CancelBasicUserAppointmentUseCase _cancelAppointmentUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _repository                = Mock.Create<IRepository<Appointment>>();
        _dateTimeService           = Mock.Create<IDateTimeService>();
        _currentUser               = Mock.Create<ICurrentUser>();
        _cancelAppointmentUseCase  = new CancelBasicUserAppointmentUseCase(
            Mock.Create<IUnitOfWork>(),
            _repository, 
            _dateTimeService,
            _currentUser);
    }

    [Test]
    public async Task ExecuteAsync_WhenAppointmentCannotBeCancelled_ShouldReturnsFailureResponse()
    {
        // Arrange
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
        Mock.Arrange(() => _repository.GetByIdAsync(Arg.AnyInt))
            .ReturnsAsync(new Appointment
            {
                UserId = 5,
                Date = new DateTime(2022, 08, 04),
                StartHour = new TimeSpan(13, 0, 0)
            });
        Mock.Arrange(() => _currentUser.UserId).Returns(5);

        // Act
        var result = await _cancelAppointmentUseCase.ExecuteAsync(default);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(Messages.AppointmentThatHasAlreadyPassedBasicUser);
    }
}
