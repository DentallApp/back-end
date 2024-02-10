namespace Plugin.ChatBot.IntegrationTests;

[SetUpFixture]
public class TestFixtureProjectSetup
{
    [OneTimeSetUp]
    public void RunBeforeAllTestFixtures()
    {
        // Allows to load the default resource in Spanish.
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
    }
}
