namespace DentallApp.Features.ChatBot.Configuration;

public class BotSettings
{
    public const string MaxDaysInCalendar = "MAX_DAYS_IN_CALENDAR";
    public string MicrosoftAppType { get; set; }
    public string MicrosoftAppId { get; set; }
    public string MicrosoftAppPassword { get; set; }
    public string MicrosoftAppTenantId { get; set; }
}
