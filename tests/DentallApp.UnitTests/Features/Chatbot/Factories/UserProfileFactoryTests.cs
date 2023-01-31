namespace DentallApp.UnitTests.Features.Chatbot.Factories;

[TestClass]
public class UserProfileFactoryTests
{
    [TestMethod]
    public void Create_WhenChannelIdStartsWithPrefix_ShouldReturnsUserProfileInstance()
    {
        var channelData  = new ChannelData();
        var channelId    = "dl_1000-2000";

        var userProfile = UserProfileFactory.Create(channelData, channelId);

        Assert.AreEqual(expected: 1000, actual: userProfile.UserId);
        Assert.AreEqual(expected: 2000, actual: userProfile.PersonId);
        Assert.IsNull(userProfile.FullName);
    }

    [TestMethod]
    public void Create_WhenChannelIdHasNotPrefix_ShouldReturnsUserProfileInstance()
    {
        var channelData = new ChannelData();
        var channelId   = "1000-2000";

        var userProfile = UserProfileFactory.Create(channelData, channelId);

        Assert.AreEqual(expected: 1000, actual: userProfile.UserId);
        Assert.AreEqual(expected: 2000, actual: userProfile.PersonId);
        Assert.IsNull(userProfile.FullName);
    }

    [DataTestMethod]
    [DataRow("dl_")]
    [DataRow("dl_1000_2000")]
    [DataRow("1000_2000")]
    [DataRow("1000")]
    [DataRow("dl_1000-2000-1-2")]
    [DataRow("1000-2000-1-2")]
    [DataRow("--")]
    [DataRow("")]
    [DataRow("  ")]
    public void Create_WhenIdentifiersCouldNotBeExtractedSeparately_ShouldThrowInvalidOperationException(string channelId)
    {
        var channelData = new ChannelData();

        void action() => UserProfileFactory.Create(channelData, channelId);

        var exception = Assert.ThrowsException<InvalidOperationException>(action);
        StringAssert.Contains(exception.Message, UserProfileFactory.IdentifiersCouldNotBeExtractedSeparatelyMessage);
    }

    [TestMethod]
    public void Create_WhenChannelDataIsNull_ShouldReturnsFullNameWithNullValue()
    {
        ChannelData channelData = default;
        var channelId = "dl_1000-2000";

        var userProfile = UserProfileFactory.Create(channelData, channelId);

        Assert.IsNull(userProfile.FullName);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("Dave Roman")]
    public void Create_WhenChannelDataIsNotNull_ShouldReturnsFullNameWithValidValue(string value)
    {
        var channelData = new ChannelData { FullName = value };
        var channelId = "dl_1000-2000";

        var userProfile = UserProfileFactory.Create(channelData, channelId);

        Assert.AreEqual(expected: value, actual: userProfile.FullName);
    }
}
