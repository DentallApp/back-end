using PhoneNumbers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DentallApp.Features.InstantMessaging;

public class WhatsAppMessaging : IInstantMessaging
{
    private readonly AppSettings _settings;

    public WhatsAppMessaging(AppSettings settings)
    {
        _settings = settings;
    }

    public async Task<string> SendMessageAsync(string phoneNumber, string message)
    {
        TwilioClient.Init(_settings.TwilioAccountSid, _settings.TwilioAuthToken);
        var util   = PhoneNumberUtil.GetInstance();
        var number = util.Parse(phoneNumber, _settings.DefaultRegion);
        var to     = util.Format(number, PhoneNumberFormat.E164);
        var messageOptions = new CreateMessageOptions(
            new Twilio.Types.PhoneNumber($"whatsapp:{to}"));
        messageOptions.From = new Twilio.Types.PhoneNumber($"whatsapp:{_settings.TwilioFromNumber}");
        messageOptions.Body = message;

        var messageResource = await MessageResource.CreateAsync(messageOptions);
        return messageResource.Body;
    }
}
