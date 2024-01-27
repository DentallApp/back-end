namespace DentallApp.IntegrationTests.ChatBot.Dialogs;

public partial class RootDialogTests
{
    [Test]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithNull_ShouldSendAnErrorMessage()
    {
        var incomingActivity = new Activity { Value = default };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        var reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectPatient);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectOffice);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectDentalService);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectDentist);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectAppointmentDate);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(Messages.SelectSchedule);
    }

    [Test]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithoutJsonFormat_ShouldSendAnErrorMessage()
    {
        var incomingActivity = new Activity { Value = "TestValue" };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        var reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectPatient);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectOffice);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectDentalService);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectDentist);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectAppointmentDate);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(Messages.SelectSchedule);
        _testClient.GetNextReply<IMessageActivity>();
    }

    [Test]
    public async Task Bot_WhenThereAreNoHoursAvailable_ShouldSendAnErrorMessage()
    {
        Mock.Arrange(() => _botService.GetAvailableHoursAsync(Arg.IsAny<AvailableTimeRangeRequest>()))
            .ReturnsAsync(new ListedResult<AvailableTimeRangeResponse>
            {
                IsSuccess = false,
                Message = Messages.NoSchedulesAvailable
            });

        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);
        replyNext.Text.Should().Be(Messages.NoSchedulesAvailable);
        replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);
    }

    [Test]
    public async Task Bot_WhenDateAndTimeAppointmentIsNotAvailable_ShouldSendAnErrorMessage()
    {
        Mock.Arrange(() => _botService.CreateScheduledAppointmentAsync(Arg.IsAny<CreateAppointmentRequest>()))
            .ReturnsAsync(new Result<CreatedId>
            {
                IsSuccess = false,
                Message = Messages.DateAndTimeAppointmentIsNotAvailable
            });

        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedSchedule());
        _testClient.GetNextReply<IMessageActivity>();
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);
        replyNext.Text.Should().Be(Messages.DateAndTimeAppointmentIsNotAvailable);
        replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);
    }
}
