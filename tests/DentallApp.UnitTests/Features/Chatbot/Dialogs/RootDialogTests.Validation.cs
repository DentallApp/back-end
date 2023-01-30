﻿namespace DentallApp.UnitTests.Features.Chatbot.Dialogs;

public partial class RootDialogTests
{
    [TestMethod]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithNull_ShouldSendAnErrorMessage()
    {
        var incomingActivity = new Activity { Value = default };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        var reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectPatientMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectOfficeMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectDentalServiceMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectDentistMessage, actual: reply.Text);

        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectAppointmentDateMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: SelectScheduleMessage, actual: reply.Text);
    }

    [TestMethod]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithoutJsonFormat_ShouldSendAnErrorMessage()
    {
        var incomingActivity = new Activity { Value = "TestValue" };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        var reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectPatientMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectOfficeMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectDentalServiceMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectDentistMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();

        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectAppointmentDateMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: SelectScheduleMessage, actual: reply.Text);
        _testClient.GetNextReply<IMessageActivity>();
    }

    [TestMethod]
    public async Task Bot_WhenThereAreNoHoursAvailable_ShouldSendAnErrorMessage()
    {
        Mock.Arrange(() => _botService.GetAvailableHoursAsync(Arg.IsAny<AvailableTimeRangePostDto>()))
            .ReturnsAsync(new Response<IEnumerable<AvailableTimeRangeDto>>
            {
                Success = false,
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

        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        Assert.AreEqual(expected: NoSchedulesAvailableMessage, actual: replyNext.Text);
        replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
    }

    [TestMethod]
    public async Task Bot_WhenDateAndTimeAppointmentIsNotAvailable_ShouldSendAnErrorMessage()
    {
        Mock.Arrange(() => _botService.CreateScheduledAppointmentAsync(Arg.IsAny<AppointmentInsertDto>()))
            .ReturnsAsync(new Response<DtoBase>
            {
                Success = false,
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

        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedSchedule());
        _testClient.GetNextReply<IMessageActivity>();
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        Assert.AreEqual(expected: DateAndTimeAppointmentIsNotAvailableMessage, actual: replyNext.Text);
        replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
    }
}