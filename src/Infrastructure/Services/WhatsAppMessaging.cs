using PhoneNumbers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DentallApp.Infrastructure.Services;

public class WhatsAppMessaging(AppSettings settings) : IInstantMessaging
{
    private CreateMessageOptions ConfigureOptions(string phoneNumber, string message)
    {
        TwilioClient.Init(settings.TwilioAccountSid, settings.TwilioAuthToken);
        var util    = PhoneNumberUtil.GetInstance();
        var number  = util.Parse(phoneNumber, settings.TwilioRegionCode);
        var to      = util.Format(number, PhoneNumberFormat.E164);
        var messageOptions = new CreateMessageOptions(
            new Twilio.Types.PhoneNumber($"whatsapp:{to}"));
        messageOptions.From = new Twilio.Types.PhoneNumber($"whatsapp:{settings.TwilioFromNumber}");
        messageOptions.Body = message;
        return messageOptions;
    }

    public async Task<string> SendMessageAsync(string phoneNumber, string message)
    {
        var messageOptions  = ConfigureOptions(phoneNumber, message);
        var messageResource = await MessageResource.CreateAsync(messageOptions);
        return messageResource.Body;
    }

    public string SendMessage(string phoneNumber, string message)
    {
        var messageOptions  = ConfigureOptions(phoneNumber, message);
        var messageResource = MessageResource.Create(messageOptions);
        return messageResource.Body;
    }
}
