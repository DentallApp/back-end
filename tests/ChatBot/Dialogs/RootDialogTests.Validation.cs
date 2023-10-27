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
        reply.Text.Should().Be(SelectPatientMessage);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(SelectOfficeMessage);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(SelectDentalServiceMessage);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(SelectDentistMessage);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(SelectAppointmentDateMessage);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Text.Should().Be(SelectScheduleMessage);
    }

    [Test]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithoutJsonFormat_ShouldSendAnErrorMessage()
    {
        var incomingActivity = new Activity { Value = "TestValue" };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        var reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectPatientMessage);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectOfficeMessage);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectDentalServiceMessage);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectDentistMessage);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectAppointmentDateMessage);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        reply.Text.Should().Be(SelectScheduleMessage);
        _testClient.GetNextReply<IMessageActivity>();
    }

    [Test]
    public async Task Bot_WhenThereAreNoHoursAvailable_ShouldSendAnErrorMessage()
    {
        Mock.Arrange(() => _botService.GetAvailableHoursAsync(Arg.IsAny<AvailableTimeRangeRequest>()))
            .ReturnsAsync(new ListedResult<AvailableTimeRangeResponse>
            {
                IsSuccess = false,
                Message = NoSchedulesAvailableMessage
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
        replyNext.Text.Should().Be(NoSchedulesAvailableMessage);
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
                Message = DateAndTimeAppointmentIsNotAvailableMessage
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
        replyNext.Text.Should().Be(DateAndTimeAppointmentIsNotAvailableMessage);
        replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);
    }
}
