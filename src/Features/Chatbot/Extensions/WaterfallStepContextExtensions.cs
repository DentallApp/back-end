namespace DentallApp.Features.Chatbot.Extensions;

public static class WaterfallStepContextExtensions
{
    private const string None            = "None";
    private const string UserInfo        = "value-userInfo";
    private const string AppointmentInfo = "value-appointmentInfo";

    public static UserProfile CreateUserProfileInstance(this WaterfallStepContext stepContext)
    {
        var channelData     = stepContext.Context.Activity.GetChannelData<ChannelData>();
        var idWithoutPrefix = stepContext.Context
                                         .Activity
                                         .From
                                         .Id
                                         .Replace(oldValue: DirectLineService.Prefix, 
                                                  newValue: string.Empty);

        var result = idWithoutPrefix.Split("-");
        var userProfile = new UserProfile
        {
            UserId   = int.Parse(result[0]),
            PersonId = int.Parse(result[1]),
            FullName = channelData?.FullName
        };
        stepContext.Values[UserInfo] = userProfile;
        return userProfile;
    }

    public static UserProfile GetUserProfile(this WaterfallStepContext stepContext)
        => (UserProfile)stepContext.Values[UserInfo];

    public static AppointmentInsertDto CreateAppointmentInstance(this WaterfallStepContext stepContext)
    {
        var appointmentInfo = new AppointmentInsertDto();
        stepContext.Values[AppointmentInfo] = appointmentInfo;
        return appointmentInfo;
    }

    public static AppointmentInsertDto GetAppointment(this WaterfallStepContext stepContext)
        => (AppointmentInsertDto)stepContext.Values[AppointmentInfo];

    /// <summary>
    /// Obtiene el valor que seleccionó el usuario desde una instancia de tipo <see cref="JObject"/>.
    /// </summary>
    /// <param name="propertyName">El nombre de la propiedad del valor.</param>
    /// <param name="stepContext"></param>
    /// <returns>El valor de la propiedad; de lo contrario, <c>null</c> sí la propiedad no está asociada con el valor.</returns>
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
    /// Obtiene el valor que seleccionó el usuario desde una instancia de tipo <see cref="string"/>.
    /// </summary>
    public static string GetValueFromString(this WaterfallStepContext stepContext)
        => stepContext.Context.Activity.Value.ToString();

    /// <summary>
    /// Regresa al anterior paso del diálogo cascada.
    /// </summary>
    public static async Task<DialogTurnResult> PreviousAsync(this WaterfallStepContext stepContext, string message, CancellationToken cancellationToken = default)
    {
        await stepContext.SendTypingActivityAsync(2000);
        await stepContext.Context.SendActivityAsync(message);
        stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 2;
        return await stepContext.NextAsync(None, cancellationToken);
    }

    /// <summary>
    /// Comprueba sí el siguiente paso no ha enviado un resultado <see cref="None" />.
    /// </summary>
    /// <returns><c>true</c> sí el siguiente paso no envió un resultado <see cref="None" />; de lo contrario, <c>false</c>.</returns>
    public static bool CheckNextStepHasNotSentResultNone(this WaterfallStepContext stepContext)
    {
        var result = stepContext.Result;
        return stepContext.Result is not string || (string)result != None;
    }

    /// <summary>
    /// Comprueba si el resultado del siguiente paso es <see cref="None" />.
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
