namespace DentallApp.Features.InstantMessaging;

public interface IInstantMessaging
{
    Task<string> SendMessageAsync(string phoneNumber, string message);
}
