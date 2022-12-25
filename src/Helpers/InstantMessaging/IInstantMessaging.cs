namespace DentallApp.Helpers.InstantMessaging;

public interface IInstantMessaging
{
    Task<string> SendMessageAsync(string phoneNumber, string message);
    string SendMessage(string phoneNumber, string message);
}
