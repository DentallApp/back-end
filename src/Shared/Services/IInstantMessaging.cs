namespace DentallApp.Shared.Services;

public interface IInstantMessaging
{
    Task<string> SendMessageAsync(string phoneNumber, string message);
    string SendMessage(string phoneNumber, string message);
}
