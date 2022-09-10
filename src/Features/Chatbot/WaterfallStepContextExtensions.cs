namespace DentallApp.Features.Chatbot;

public static class WaterfallStepContextExtensions
{
    private const string None           = "None";
    private const string UserInfo       = "value-userInfo";
    private const string AppoinmentInfo = "value-appoinmentInfo";

    public static UserProfile CreateUserProfileInstance(this WaterfallStepContext stepContext)
    {
        var userProfile  = stepContext.Context.Activity.GetChannelData<UserProfile>();
        userProfile.Id   = int.Parse(stepContext.Context.Activity.From.Id);
        userProfile.Name = stepContext.Context.Activity.From.Name;
        stepContext.Values[UserInfo] = userProfile;
        return userProfile;
    }

    public static UserProfile GetUserProfile(this WaterfallStepContext stepContext)
        => (UserProfile)stepContext.Values[UserInfo];

    public static AppoinmentInsertDto CreateAppoinmentInstance(this WaterfallStepContext stepContext)
    {
        var appoinmentInfo = new AppoinmentInsertDto();
        stepContext.Values[AppoinmentInfo] = appoinmentInfo;
        return appoinmentInfo;
    }

    public static AppoinmentInsertDto GetAppoinment(this WaterfallStepContext stepContext)
        => (AppoinmentInsertDto)stepContext.Values[AppoinmentInfo];

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
        await stepContext.Context.SendActivityAsync(message);
        stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 2;
        return await stepContext.NextAsync(None, cancellationToken);
    }

    /// <summary>
    /// Comprueba sí el siguiente paso no ha enviado un resultado <see cref="None" />.
    /// </summary>
    /// <returns><c>true</c> sí el siguiente paso no envió un resultado; de lo contrario, <c>false</c>.</returns>
    public static bool CheckNextStepHasNotSentResult(this WaterfallStepContext stepContext)
    {
        var result = stepContext.Result;
        return stepContext.Result is not string || (string)result != None;
    }
}
