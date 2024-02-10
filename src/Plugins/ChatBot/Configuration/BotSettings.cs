namespace DentallApp.Features.ChatBot.Configuration;

public class BotSettings
{
    public const string MaxDaysInCalendar = "MAX_DAYS_IN_CALENDAR";
    public string MicrosoftAppType { get; set; } = string.Empty;
    public string MicrosoftAppId { get; set; } = string.Empty;
    public string MicrosoftAppPassword { get; set; } = string.Empty;
    public string MicrosoftAppTenantId { get; set; } = string.Empty;
}
