namespace DentallApp.Features.Chatbot;

public static class WaterfallStepContextExtensions
{
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
    /// <param name="key">El nombre de la clave del valor.</param>
    /// <param name="stepContext"></param>
    public static string GetValueFromJObject(this WaterfallStepContext stepContext, string key)
    {
        var value = JObject.Parse(stepContext.Context.Activity.Value.ToString());
        return (string)value[key];
    }

    /// <summary>
    /// Obtiene el valor que seleccionó el usuario desde una instancia de tipo <see cref="string"/>.
    /// </summary>
    public static string GetValueFromString(this WaterfallStepContext stepContext)
        => stepContext.Context.Activity.Value.ToString();
}
