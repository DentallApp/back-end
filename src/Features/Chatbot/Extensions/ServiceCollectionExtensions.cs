namespace DentallApp.Features.Chatbot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        var storage = new MemoryStorage();

        // Create the Conversation state passing in the storage layer.
        var conversationState = new ConversationState(storage);
        services.AddSingleton(conversationState);

        services.AddSingleton<RootDialog>();
        services.AddSingleton<IAppointmentBotService, AppointmentBotService>();
        services.AddScoped<IBotQueryRepository, BotQueryRepository>();

        // Create the Bot Framework Authentication to be used with the Bot Adapter.
        services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();

        // Create the Bot Adapter with error handling enabled.
        services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

        // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
        services.AddTransient<IBot, AppointmentBotHandler<RootDialog>>();

        return services;
    }
}
