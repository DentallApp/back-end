namespace DentallApp.IntegrationTests.ChatBot.Dialogs;

public partial class RootDialogTests
{
    private DialogTestClient _testClient;
    private IDateTimeService _dateTimeService;
    private IAppointmentBotService _botService;

    [SetUp]
    public void TestInitialize()
    {
        _botService       = CreateBotServiceMock();
        _dateTimeService  = Mock.Create<IDateTimeService>();
        _testClient       = new(Channels.Webchat, new RootDialog(_botService, _dateTimeService));
        Environment.SetEnvironmentVariable(AppSettings.MaxDaysInCalendar, "60");
    }

    [Test]
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
        reply.Type.Should().Be(ActivityTypes.Typing);

        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);

        var content   = JObject.Parse(replyNext.Attachments[0].Content.ToString());
        var choices   = content.SelectToken("body[1].choices").ToObject<List<AdaptiveChoice>>();
        choices.Should().HaveCount(1);
        choices[0].Title.Should().Be(choiceType);
        choices[0].Value.Should().Be(Id);
    }

    private async Task SendReplyWithInputDateAsync(Activity incomingActivity)
    {
        Mock.Arrange(() => _dateTimeService.Now).Returns(new DateTime(2023, 01, 01));

        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Type.Should().Be(ActivityTypes.Typing);

        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Text.Should().Be(new ShowScheduleToUserSuccess(Schedule).Message);
        replyNext.Type.Should().Be(ActivityTypes.Message);

        replyNext     = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);

        var content   = JObject.Parse(replyNext.Attachments[0].Content.ToString());
        var type      = content.SelectToken("body[1].type").ToObject<string>();
        type.Should().Be("Input.Date");
        var min       = content.SelectToken("body[1].min").ToObject<string>();
        min.Should().Be("2023-01-01");
        var max       = content.SelectToken("body[1].max").ToObject<string>();
        max.Should().Be("2023-03-02");
    }

    private async Task SendReplyWithHeroCardAsync(Activity incomingActivity)
    {
        var reply     = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Type.Should().Be(ActivityTypes.Typing);

        var replyNext = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Text.Should().Be(new TotalHoursAvailableSuccess(totalHours: 1).Message);
        replyNext.Type.Should().Be(ActivityTypes.Message);

        replyNext     = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Type.Should().Be(ActivityTypes.Message);

        var heroCard  = (HeroCard)replyNext.Attachments[0].Content;
        heroCard.Buttons[0].Title.Should().Be($"{StartHour} - {EndHour}");
        heroCard.Buttons[0].Value.Should().Be($"{StartHour} - {EndHour}");
    }

    private async Task SendLastReplyAsync(Activity incomingActivity)
    {
        var reply         = await _testClient.SendActivityAsync<IMessageActivity>(incomingActivity);
        reply.Type.Should().Be(ActivityTypes.Typing);

        var replyNext     = _testClient.GetNextReply<IMessageActivity>();
        var rangeToPayMsg = new PaymentSuccess(PriceMin, PriceMax).Message;
        replyNext.Text
                 .Should()
                 .Be(new SuccessfullyScheduledAppointmentSuccess(rangeToPayMsg).Message);
        replyNext.Type.Should().Be(ActivityTypes.Message);

        replyNext         = _testClient.GetNextReply<IMessageActivity>();
        replyNext.Text.Should().Be(Messages.ThanksForUsingService);
        replyNext.Type.Should().Be(ActivityTypes.Message);
    }
}
