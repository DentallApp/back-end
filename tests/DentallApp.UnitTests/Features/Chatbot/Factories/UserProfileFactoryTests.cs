namespace DentallApp.UnitTests.Features.Chatbot.Factories;

[TestClass]
public class UserProfileFactoryTests
{
    [TestMethod]
    public void Create_WhenChannelIdStartsWithPrefix_ShouldReturnsUserProfileInstance()
    {
        // Arrange
        var channelData  = new ChannelData();
        var channelId    = "dl_1000-2000";

        // Act
        var userProfile = UserProfileFactory.Create(channelData, channelId);

        // Asserts
        userProfile.UserId.Should().Be(1000);
        userProfile.PersonId.Should().Be(2000);
        userProfile.FullName.Should().BeNull();
    }

    [TestMethod]
    public void Create_WhenChannelIdHasNotPrefix_ShouldReturnsUserProfileInstance()
    {
        // Arrange
        var channelData = new ChannelData();
        var channelId   = "1000-2000";

        // Act
        var userProfile = UserProfileFactory.Create(channelData, channelId);

        // Asserts
        userProfile.UserId.Should().Be(1000);
        userProfile.PersonId.Should().Be(2000);
        userProfile.FullName.Should().BeNull();
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
        // Arrange
        var channelData = new ChannelData();
        var expectedMessage = UserProfileFactory.IdentifiersCouldNotBeExtractedSeparatelyMessage;

        // Act
        Action act = () => UserProfileFactory.Create(channelData, channelId);

        // Assert
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage(expectedMessage);
    }

    [TestMethod]
    public void Create_WhenChannelDataIsNull_ShouldReturnsFullNameWithNullValue()
    {
        // Arrange
        ChannelData channelData = default;
        var channelId = "dl_1000-2000";

        // Act
        var userProfile = UserProfileFactory.Create(channelData, channelId);

        // Assert
        userProfile.FullName.Should().BeNull();
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("Dave Roman")]
    public void Create_WhenChannelDataIsNotNull_ShouldReturnsFullNameWithValidValue(string expectedValue)
    {
        // Arrange
        var channelData = new ChannelData { FullName = expectedValue };
        var channelId = "dl_1000-2000";

        // Act
        var userProfile = UserProfileFactory.Create(channelData, channelId);

        // Assert
        userProfile.FullName.Should().Be(expectedValue);
    }
}
