namespace DentallApp.Infrastructure.Services;

public class FakeInstantMessaging(ILogger<FakeInstantMessaging> logger) : IInstantMessaging
{
    public string SendMessage(string phoneNumber, string message)
    {
        const string template =
        """
        PhoneNumber: {phoneNumber}
        Message:
        {message}
        """;
        logger.LogInformation(template, phoneNumber, message);
        return string.Empty;
    }

    public Task<string> SendMessageAsync(string phoneNumber, string message)
    {
        return Task.FromResult(SendMessage(phoneNumber, message));
    }
}
