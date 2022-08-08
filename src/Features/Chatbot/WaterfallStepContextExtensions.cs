namespace DentallApp.Features.Chatbot;

public static class WaterfallStepContextExtensions
{
    private const string UserInfo = "value-userInfo";

    public static UserProfile CreateUserProfile(this WaterfallStepContext stepContext)
    {
        var userProfile  = stepContext.Context.Activity.GetChannelData<UserProfile>();
        userProfile.Id   = int.Parse(stepContext.Context.Activity.From.Id);
        userProfile.Name = stepContext.Context.Activity.From.Name;
        stepContext.Values[UserInfo] = userProfile;
        return userProfile;
    }

    public static UserProfile GetUserProfile(this WaterfallStepContext stepContext)
        => (UserProfile)stepContext.Values[UserInfo];
}
