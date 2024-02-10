namespace Plugin.Twilio.WhatsApp;

public class WhatsAppMessaging(TwilioSettings settings) : IInstantMessaging
{
    private CreateMessageOptions ConfigureOptions(string phoneNumber, string message)
    {
        TwilioProvider.TwilioClient.Init(settings.TwilioAccountSid, settings.TwilioAuthToken);
        var util    = PhoneNumberUtil.GetInstance();
        var number  = util.Parse(phoneNumber, settings.TwilioRegionCode);
        var to      = util.Format(number, PhoneNumberFormat.E164);
        var messageOptions = new CreateMessageOptions(
            new TwilioProvider.Types.PhoneNumber($"whatsapp:{to}"));
        messageOptions.From = new TwilioProvider.Types.PhoneNumber($"whatsapp:{settings.TwilioFromNumber}");
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
