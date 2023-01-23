namespace DentallApp.Tests.Configuration;

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
    public void GetProviderName_WhenBaseUrlIsEmptyOrWhiteSpace_ShouldReturnsDefaultProviderName()
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, " ");
        var settings = _envBinder.Bind<DirectLineSettings>();

        var providerName = settings.GetProviderName();

        Assert.AreEqual(expected: DirectLineSettings.DefaultProviderName, actual: providerName);
    }

    [DataTestMethod]
    [DataRow(DirectLineSettings.DefaultBaseUrl)]
    [DataRow(DirectLineSettings.DefaultBaseUrl + "///")]
    public void GetProviderName_WhenBaseUrlStartsWithDefaultBaseUrl_ShouldReturnsDefaultProviderName(string baseUrl)
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        var providerName = settings.GetProviderName();

        Assert.AreEqual(expected: DirectLineSettings.DefaultProviderName, actual: providerName);
    }

    [DataTestMethod]
    [DataRow(LocalHostBaseUrl + "/")]
    [DataRow(LocalHostBaseUrl)]
    [DataRow(LocalHostBaseUrl + "///")]
    public void GetProviderName_WhenBaseUrlNotStartsWithDefaultBaseUrl_ShouldReturnsInDirectLineService(string baseUrl)
    {
        Environment.SetEnvironmentVariable(DirectLineSettings.DirectLineBaseUrlSetting, baseUrl);
        var settings = _envBinder.Bind<DirectLineSettings>();

        var providerName = settings.GetProviderName();

        Assert.AreEqual(expected: nameof(InDirectLineService), actual: providerName);
    }
}
