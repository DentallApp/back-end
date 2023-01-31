namespace DentallApp.Features.Chatbot.Factories;

public class UserProfileFactory
{
    public const string IdentifiersCouldNotBeExtractedSeparatelyMessage
        = "The 'user ID' and 'person ID' could not be extracted separately from channel ID.\n" +
          "This error may be caused by not following the format: " +
          "{userID}-{personID}";

    public static UserProfile Create(ChannelData channelData, string channelId)
    {
        var idWithoutPrefix = channelId.Replace(oldValue: DirectLineService.Prefix, newValue: string.Empty);
        var identifiers     = idWithoutPrefix.Split("-");
        if (identifiers.Length != 2)
            throw new InvalidOperationException(IdentifiersCouldNotBeExtractedSeparatelyMessage);
        var userProfile     = new UserProfile
        {
            UserId   = int.Parse(identifiers[0]),
            PersonId = int.Parse(identifiers[1]),
            FullName = channelData?.FullName
        };
        return userProfile;
    }
}
