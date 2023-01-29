namespace DentallApp.UnitTests.Features.Chatbot.Dialogs;

[TestClass]
public partial class RootDialogTests
{
    private DialogTestClient _testClient;
    private IDateTimeProvider _dateTimeProvider;

    [TestInitialize]
    public void TestInitialize()
    {
        var botService    = CreateMock();
        _dateTimeProvider = Mock.Create<IDateTimeProvider>();
        _testClient = new(Channels.Webchat, new RootDialog(botService, _dateTimeProvider));
    }

    [TestMethod]
    public async Task Bot_WhenAnIncomingActivityIsSent_ShouldRespondWithAnOutgoingActivity()
    {
        await SendReplyWithChoiceSetAsync(choiceType: PatientName,       incomingActivity: CreateInitialActivity());
        await SendReplyWithChoiceSetAsync(choiceType: OfficeName,        incomingActivity: CreateActivityWithSelectedPatientId());
        await SendReplyWithChoiceSetAsync(choiceType: DentalServiceName, incomingActivity: CreateActivityWithSelectedOfficeId());
        await SendReplyWithChoiceSetAsync(choiceType: DentistName,       incomingActivity: CreateActivityWithSelectedDentalServiceId());
        await SendReplyWithInputDateAsync(incomingActivity: CreateActivityWithSelectedDentistId());
        await SendReplyWithHeroCardAsync(incomingActivity: CreateActivityWithSelectedDate());
        await SendLastReplyAsync(incomingActivity: CreateActivityWithSelectedSchedule());
    }

    /// <summary>
    /// Sends a choice set type response to the client.
    /// </summary>
    /// <param name="choiceType">The type of choice to send to client.</param>
    /// <param name="incomingActivity">The incoming activity sent by the client.</param>
    /// <returns></returns>
    private async Task SendReplyWithChoiceSetAsync(string choiceType, Activity incomingActivity)
    {
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: ActivityTypes.Typing, actual: reply.Type);
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        var content   = JObject.Parse(replyNext.Attachments[0].Content.ToString());
        var choices   = content.SelectToken("body[1].choices").ToObject<List<AdaptiveChoice>>();
        Assert.AreEqual(expected: 1,          actual: choices.Count);
        Assert.AreEqual(expected: choiceType, actual: choices[0].Title);
        Assert.AreEqual(expected: Id,         actual: choices[0].Value);
    }

    private async Task SendReplyWithInputDateAsync(Activity incomingActivity)
    {
        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2023, 01, 01));
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: ActivityTypes.Typing, actual: reply.Type);
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: string.Format(ShowScheduleToUserMessage, Schedule), actual: replyNext.Text);
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        replyNext     = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        var content   = JObject.Parse(replyNext.Attachments[0].Content.ToString());
        var type      = content.SelectToken("body[1].type").ToObject<string>();
        Assert.AreEqual(expected: "Input.Date", actual: type);
        var min       = content.SelectToken("body[1].min").ToObject<string>();
        Assert.AreEqual(expected: "2023-01-01", actual: min);
        var max       = content.SelectToken("body[1].max").ToObject<string>();
        Assert.AreEqual(expected: "2023-03-02", actual: max);
    }

    private async Task SendReplyWithHeroCardAsync(Activity incomingActivity)
    {
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: ActivityTypes.Typing, actual: reply.Type);
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: string.Format(TotalHoursAvailableMessage, 1), actual: replyNext.Text);
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        replyNext     = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        var heroCard  = (HeroCard)replyNext.Attachments[0].Content;
        Assert.AreEqual(expected: $"{StartHour} - {EndHour}", actual: heroCard.Buttons[0].Title);
        Assert.AreEqual(expected: $"{StartHour} - {EndHour}", actual: heroCard.Buttons[0].Value);
    }

    private async Task SendLastReplyAsync(Activity incomingActivity)
    {
        var reply         = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        Assert.AreEqual(expected: ActivityTypes.Typing,  actual: reply.Type);
        var replyNext     = _testClient.GetNextReply<IMessageActivity>();
        var rangeToPayMsg = string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax);
        Assert.AreEqual(expected: string.Format(SuccessfullyScheduledAppointmentMessage, rangeToPayMsg), actual: replyNext.Text);
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        replyNext         = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ThanksForUsingServiceMessage, actual: replyNext.Text);
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
    }
}
