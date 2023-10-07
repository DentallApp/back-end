namespace DentallApp.Features.ChatBot.Extensions;

public static class ConfigurationExtensions
{
    public static IConfiguration SetBotCredentials(this IConfiguration configuration)
    {
        var botSettings = new EnvBinder().Bind<BotSettings>();
        configuration[MicrosoftAppCredentials.MicrosoftAppTypeKey]     = botSettings.MicrosoftAppType    .Trim();
        configuration[MicrosoftAppCredentials.MicrosoftAppIdKey]       = botSettings.MicrosoftAppId      .Trim();
        configuration[MicrosoftAppCredentials.MicrosoftAppPasswordKey] = botSettings.MicrosoftAppPassword.Trim();
        configuration[MicrosoftAppCredentials.MicrosoftAppTenantIdKey] = botSettings.MicrosoftAppTenantId.Trim();
        return configuration;
    }
}
