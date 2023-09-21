namespace DentallApp.UnitTests.Features.Appointments;

public class CancelBasicUserAppointmentUseCaseTests
{
    private IRepository<Appointment> _repository;
    private IDateTimeService _dateTimeService;
    private CancelBasicUserAppointmentUseCase _cancelAppointmentUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _repository                = Mock.Create<IRepository<Appointment>>();
        _dateTimeService           = Mock.Create<IDateTimeService>();
        _cancelAppointmentUseCase  = new CancelBasicUserAppointmentUseCase(
            Mock.Create<IUnitOfWork>(),
            _repository, 
            _dateTimeService);
    }

    [Test]
    public async Task Execute_WhenAppointmentCannotBeCancelled_ShouldReturnsFailureResponse()
    {
        // Arrange
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
        Mock.Arrange(() => _repository.GetByIdAsync(Arg.AnyInt))
            .ReturnsAsync((int id) => new Appointment
            {
                Date = new DateTime(2022, 08, 04),
                StartHour = new TimeSpan(13, 0, 0)
            });

        // Act
        var response = await _cancelAppointmentUseCase.Execute(default, default);

        // Asserts
        response.Success.Should().BeFalse();
        response.Message.Should().Be(AppointmentThatHasAlreadyPassedBasicUserMessage);
    }
}
