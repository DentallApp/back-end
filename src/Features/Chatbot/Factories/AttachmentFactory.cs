namespace DentallApp.Features.Chatbot.Factories;
using Attachment = Microsoft.Bot.Schema.Attachment;

public class AttachmentFactory
{
    public static Attachment Create(string json, string contentType)
        => new()
        {
            ContentType = contentType,
            Content = JsonConvert.DeserializeObject(json)
        };
}
