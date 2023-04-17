namespace DentallApp.UnitTests.Features.Chatbot.DirectLine;

[TestClass]
public class DirectLineSettingsTests
{
    private const string LocalHostBaseUrl = "http://localhost:5000";
    private IEnvBinder _envBinder;

    [TestInitialize]
    public void TestInitialize()
    {
        _envBinder = new EnvBinder().AllowBindNonPublicProperties();
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineSecretSetting, "SECRET");
    }

    [TestMethod]
    public void GetDirectLineBaseUrl_WhenDirectLineBaseUrlIsEmptyOrWhiteSpace_ShouldReturnsDefaultBaseUrl()
    {
        // Arrange
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, " ");
        var settings = _envBinder.Bind<DirectLineSettings>();

        // Act
        var baseUrl = settings.GetDirectLineBaseUrl();

        // Assert
        baseUrl.Should().Be(DirectLineSettings.DefaultBaseUrl);
    }

    [DataTestMethod]
    [DataRow(LocalHostBaseUrl + "/")]
    [DataRow(LocalHostBaseUrl)]
    [DataRow(LocalHostBaseUrl + "///")]
    public void GetDirectLineBaseUrl_WhenDirectLineBaseUrlIsNotEmpty_ShouldReturnsBaseUrlWithSlashAtEnd(string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, value);
        var settings = _envBinder.Bind<DirectLineSettings>();

        // Act
        var baseUrl = settings.GetDirectLineBaseUrl();

        // Assert
        baseUrl.Should().Be("http://localhost:5000/");
    }

    [TestMethod]
    public void GetServiceName_WhenBaseUrlIsEmptyOrWhiteSpace_ShouldReturnsDefaultServiceName()
    {
        // Arrange
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, " ");
        var settings = _envBinder.Bind<DirectLineSettings>();

        // Act
        var serviceName = settings.GetServiceName();

        // Assert
        serviceName.Should().Be(DirectLineSettings.DefaultServiceName);
    }

    [DataTestMethod]
    [DataRow(DirectLineSettings.DefaultBaseUrl)]
    [DataRow(DirectLineSettings.DefaultBaseUrl + "///")]
    public void GetServiceName_WhenBaseUrlStartsWithDefaultBaseUrl_ShouldReturnsDefaultServiceName(string baseUrl)
    {
        // Arrange
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        // Act
        var serviceName = settings.GetServiceName();

        // Assert
        serviceName.Should().Be(DirectLineSettings.DefaultServiceName);
    }

    [DataTestMethod]
    [DataRow(LocalHostBaseUrl + "/")]
    [DataRow(LocalHostBaseUrl)]
    [DataRow(LocalHostBaseUrl + "///")]
    public void GetServiceName_WhenBaseUrlNotStartsWithDefaultBaseUrl_ShouldReturnsInDirectLineService(string baseUrl)
    {
        // Arrange
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        // Act
        var serviceName = settings.GetServiceName();

        // Assert
        serviceName.Should().Be(nameof(InDirectLineService));
    }
}
