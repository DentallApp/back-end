﻿namespace DentallApp.Features.Chatbot.Factories;

/// <summary>
/// A factory that can create <see cref="UserProfile" /> instances.
/// </summary>
public class UserProfileFactory
{
    public const string IdentifiersCouldNotBeExtractedSeparatelyMessage
        = "The 'user ID' and 'person ID' could not be extracted separately from channel ID.\n" +
          "This error may be caused by not following the format: " +
          "{userID}-{personID}";

    /// <summary>
    /// Creates an instance of type <see cref="UserProfile" />.
    /// </summary>
    /// <param name="channelData">The channel data.</param>
    /// <param name="channelId">The channel ID.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// The identifiers cannot be extracted separately from the channel ID.
    /// </exception>
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
