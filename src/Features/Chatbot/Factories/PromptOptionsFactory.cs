namespace DentallApp.Features.Chatbot.Factories;
using Attachment = Microsoft.Bot.Schema.Attachment;

public class PromptOptionsFactory
{
    public static PromptOptions Create(Attachment attachment, string errorMessage)
        => new()
        {
            Prompt = new Activity
            {
                Attachments = new List<Attachment>() { attachment },
                Type = ActivityTypes.Message,

            },
            RetryPrompt = MessageFactory.Text(errorMessage)
        };
}
