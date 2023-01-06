namespace DentallApp.Tests.Features.Chatbot;

[TestClass]
public class RootDialogTests
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
        await SendReplyWithChoiceSetAsync(cardType: PatientName, CreateInitialActivity());

        var userSelectedValue = "{ patientId: 1 }";
        var activity = new Activity { Value = JObject.Parse(userSelectedValue) };
        await SendReplyWithChoiceSetAsync(cardType: OfficeName, activity);

        userSelectedValue = "{ officeId: 1 }";
        activity = new Activity { Value = JObject.Parse(userSelectedValue) };
        await SendReplyWithChoiceSetAsync(cardType: DentalServiceName, activity);

        userSelectedValue = "{ dentalServiceId: 1 }";
        activity = new Activity { Value = JObject.Parse(userSelectedValue) };
        await SendReplyWithChoiceSetAsync(cardType: DentistName, activity);

        userSelectedValue = "{ dentistId: 1 }";
        activity = new Activity { Value = JObject.Parse(userSelectedValue) };
        await SendReplyWithInputDateAsync(activity);

        userSelectedValue = "{ date: '2023-01-06' }";
        activity = new Activity { Value = JObject.Parse(userSelectedValue) };
        await SendReplyWithHeroCardAsync(activity);

        userSelectedValue = "07:00 - 08:00";
        activity = new Activity { Value = userSelectedValue };
        await SendLastReplyAsync(activity);
    }

    private Activity CreateInitialActivity()
        => new()
        {
            Text = "Hi",
            From = new ChannelAccount { Id = "1", Name = "daveseva2010@hotmail.es" },
            ChannelData = JObject.Parse(@"
            {
                personId: 1,
                fullName: 'Dave Roman'
            }")
        };

    private async Task SendReplyWithChoiceSetAsync(string cardType, Activity activity)
    {
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: ActivityTypes.Typing, actual: reply.Type);
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        var content   = JObject.Parse(replyNext.Attachments[0].Content.ToString());
        var choices   = content.SelectToken("body[1].choices").ToObject<List<AdaptiveChoice>>();
        Assert.AreEqual(expected: 1,        actual: choices.Count);
        Assert.AreEqual(expected: cardType, actual: choices[0].Title);
        Assert.AreEqual(expected: Id,       actual: choices[0].Value);
    }

    private async Task SendReplyWithInputDateAsync(Activity activity)
    {
        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
        Mock.Arrange(() => _dateTimeProvider.Now).Returns(new DateTime(2023, 01, 01));
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(activity);
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

    private async Task SendReplyWithHeroCardAsync(Activity activity)
    {
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(activity);
        Assert.AreEqual(expected: ActivityTypes.Typing, actual: reply.Type);
        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: string.Format(TotalHoursAvailableMessage, 1), actual: replyNext.Text);
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        replyNext     = _testClient.GetNextReply<IMessageActivity>();
        Assert.AreEqual(expected: ActivityTypes.Message, actual: replyNext.Type);
        var heroCard = (HeroCard)replyNext.Attachments[0].Content;
        Assert.AreEqual(expected: $"{StartHour} - {EndHour}", actual: heroCard.Buttons[0].Title);
        Assert.AreEqual(expected: $"{StartHour} - {EndHour}", actual: heroCard.Buttons[0].Value);
    }

    private async Task SendLastReplyAsync(Activity activity)
    {
        var reply         = await _testClient.SendActivityAsync<IMessageActivity>(activity);
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
