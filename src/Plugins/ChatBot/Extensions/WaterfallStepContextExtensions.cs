namespace Plugin.ChatBot.Extensions;

public static class WaterfallStepContextExtensions
{
    private const string None            = "None";
    private const string UserInfo        = "value-userInfo";
    private const string AppointmentInfo = "value-appointmentInfo";

    public static AuthenticatedUser CreateUserProfileInstance(this WaterfallStepContext stepContext)
    {
        var channelData              = stepContext.Context.Activity.GetChannelData<ChannelData>();
        var userProfile              = UserProfileFactory.Create(channelData, stepContext.Context.Activity.From.Id);
        stepContext.Values[UserInfo] = userProfile;
        return userProfile;
    }

    public static AuthenticatedUser GetUserProfile(this WaterfallStepContext stepContext)
        => (AuthenticatedUser)stepContext.Values[UserInfo];

    public static CreateAppointmentRequest CreateAppointmentInstance(this WaterfallStepContext stepContext)
    {
        var appointmentInfo = new CreateAppointmentRequest();
        stepContext.Values[AppointmentInfo] = appointmentInfo;
        return appointmentInfo;
    }

    public static CreateAppointmentRequest GetAppointment(this WaterfallStepContext stepContext)
        => (CreateAppointmentRequest)stepContext.Values[AppointmentInfo];

    /// <summary>
    /// Gets the value that the user selected from an instance of type <see cref="JObject"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="stepContext"></param>
    /// <returns>Property value; otherwise <c>null</c> if the property is not associated with the value.</returns>
    public static string GetValueFromJObject(this WaterfallStepContext stepContext, string propertyName)
    {
        try
        {
            var jObject = JObject.Parse(stepContext.Context.Activity.Value.ToString());
            return jObject.TryGetValue(propertyName, out JToken value) ? (string)value : null;
        }
        catch(JsonReaderException)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the value that the user selected from an instance of type <see cref="string"/>.
    /// </summary>
    public static string GetValueFromString(this WaterfallStepContext stepContext)
        => stepContext.Context.Activity.Value.ToString();

    /// <summary>
    /// Returns to the previous step of the waterfall dialog.
    /// </summary>
    public static async Task<DialogTurnResult> PreviousAsync(this WaterfallStepContext stepContext, string message, CancellationToken cancellationToken = default)
    {
        await stepContext.SendTypingActivityAsync(2000);
        await stepContext.Context.SendActivityAsync(message);
        stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 2;
        return await stepContext.NextAsync(None, cancellationToken);
    }

    /// <summary>
    /// Checks if the next step has not sent a result called <see cref="None" />.
    /// </summary>
    /// <returns><c>true</c> if the next step has not sent a result; otherwise, <c>false</c>.</returns>
    public static bool CheckNextStepHasNotSentResultNone(this WaterfallStepContext stepContext)
    {
        var result = stepContext.Result;
        return stepContext.Result is not string || (string)result != None;
    }

    /// <summary>
    /// Checks if the result of the next step is <see cref="None" />.
    /// </summary>
    public static bool CheckIfResultNextStepIsNone(this WaterfallStepContext stepContext)
        => !stepContext.CheckNextStepHasNotSentResultNone();

    public static async Task SendTypingActivityAsync(this WaterfallStepContext stepContext, int millisecondsDelay = 3000)
    {
        var typingActivity = stepContext.Context.Activity.CreateReply();
        typingActivity.Type = ActivityTypes.Typing;
        await stepContext.Context.SendActivityAsync(typingActivity);
        await Task.Delay(millisecondsDelay);
    }

    public static async Task<DialogTurnResult> PromptAsync(this WaterfallStepContext stepContext, string dialogId, string retryMessage)
        => await stepContext.PromptAsync(
                dialogId,
                new PromptOptions
                {
                    Prompt = new Activity { Type = ActivityTypes.Message },
                    RetryPrompt = MessageFactory.Text(retryMessage)
                },
                default
            );

}
