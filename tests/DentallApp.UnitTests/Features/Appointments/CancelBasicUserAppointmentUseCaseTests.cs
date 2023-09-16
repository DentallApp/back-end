namespace DentallApp.UnitTests.Features.Appointments;

public class CancelBasicUserAppointmentUseCaseTests
{
    private IRepository<Appointment> _repository;
    private IDateTimeProvider _dateTimeProvider;
    private CancelBasicUserAppointmentUseCase _cancelAppointmentUseCase;

    [SetUp]
    public void TestInitialize()
    {
        _repository                = Mock.Create<IRepository<Appointment>>();
        _dateTimeProvider          = Mock.Create<IDateTimeProvider>();
        _cancelAppointmentUseCase  = new CancelBasicUserAppointmentUseCase(
            Mock.Create<IUnitOfWork>(),
            _repository, 
            _dateTimeProvider);
    }

    [Test]
    public async Task Execute_WhenAppointmentCannotBeCancelled_ShouldReturnsFailureResponse()
    {
        // Arrange
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2022, 08, 04, 15, 0, 0));
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
