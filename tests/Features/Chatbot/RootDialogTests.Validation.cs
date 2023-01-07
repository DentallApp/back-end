namespace DentallApp.Tests.Features.Chatbot;

public partial class RootDialogTests
{
    [TestMethod]
    public async Task Bot_WhenIncomingActivityHasValuePropertyWithNull_ShouldSendAnErrorMessage()
    {
        var activity = new Activity { Value = default };
        await _testClient.SendActivityAsync<IMessageActivity>(CreateInitialActivity());
        _testClient.GetNextReply<IMessageActivity>();
        var reply = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: SelectPatientMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedPatientId());
        _testClient.GetNextReply<IMessageActivity>();
        reply     = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: SelectOfficeMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedOfficeId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: SelectDentalServiceMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentalServiceId());
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: SelectDentistMessage, actual: reply.Text);

        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDentistId());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: SelectAppointmentDateMessage, actual: reply.Text);

        await _testClient.SendActivityAsync<IMessageActivity>(CreateActivityWithSelectedDate());
        _testClient.GetNextReply<IMessageActivity>();
        _testClient.GetNextReply<IMessageActivity>();
        reply = await _testClient.SendActivityAsync<IMessageActivity>(activity);
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
}
