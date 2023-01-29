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
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, " ");
        var settings = _envBinder.Bind<DirectLineSettings>();

        var baseUrl = settings.GetDirectLineBaseUrl();

        Assert.AreEqual(expected: DirectLineSettings.DefaultBaseUrl, actual: baseUrl);
    }

    [DataTestMethod]
    [DataRow(LocalHostBaseUrl + "/")]
    [DataRow(LocalHostBaseUrl)]
    [DataRow(LocalHostBaseUrl + "///")]
    public void GetDirectLineBaseUrl_WhenDirectLineBaseUrlIsNotEmpty_ShouldReturnsBaseUrlWithSlashAtEnd(string value)
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, value);
        var settings = _envBinder.Bind<DirectLineSettings>();

        var baseUrl = settings.GetDirectLineBaseUrl();

        Assert.AreEqual(expected: "http://localhost:5000/", actual: baseUrl);
    }

    [TestMethod]
    public void GetServiceName_WhenBaseUrlIsEmptyOrWhiteSpace_ShouldReturnsDefaultServiceName()
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, " ");
        var settings = _envBinder.Bind<DirectLineSettings>();

        var serviceName = settings.GetServiceName();

        Assert.AreEqual(expected: DirectLineSettings.DefaultServiceName, actual: serviceName);
    }

    [DataTestMethod]
    [DataRow(DirectLineSettings.DefaultBaseUrl)]
    [DataRow(DirectLineSettings.DefaultBaseUrl + "///")]
    public void GetServiceName_WhenBaseUrlStartsWithDefaultBaseUrl_ShouldReturnsDefaultServiceName(string baseUrl)
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        var serviceName = settings.GetServiceName();

        Assert.AreEqual(expected: DirectLineSettings.DefaultServiceName, actual: serviceName);
    }

    [DataTestMethod]
    [DataRow(LocalHostBaseUrl + "/")]
    [DataRow(LocalHostBaseUrl)]
    [DataRow(LocalHostBaseUrl + "///")]
    public void GetServiceName_WhenBaseUrlNotStartsWithDefaultBaseUrl_ShouldReturnsInDirectLineService(string baseUrl)
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        var serviceName = settings.GetServiceName();

        Assert.AreEqual(expected: nameof(InDirectLineService), actual: serviceName);
    }
}
