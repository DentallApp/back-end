namespace DentallApp.Features.Chatbot.Factories;
using Attachment = Microsoft.Bot.Schema.Attachment;

public class AdaptiveCardFactory
{
    private static PromptOptions CreatePromptOptions(Attachment attachment, string errorMessage)
    {
        var opts = new PromptOptions
        {
            Prompt = new Activity
            {
                Attachments = new List<Attachment>() { attachment },
                Type = ActivityTypes.Message,

            },
            RetryPrompt = MessageFactory.Text(errorMessage)
        };
        return opts;
    }

    public static PromptOptions CreateSingleChoiceCard(string cardJson, List<AdaptiveChoice> choices)
    {
        var card = AdaptiveCard.FromJson(cardJson).Card;
        var choiceSetInput = card.Body[1] as AdaptiveChoiceSetInput;
        choiceSetInput.Choices = choices;

        var attachment = new Attachment
        {
            ContentType = AdaptiveCard.ContentType,
            Content = JsonConvert.DeserializeObject(card.ToJson())
        };

        return CreatePromptOptions(attachment, choiceSetInput.ErrorMessage);
    }

    public static PromptOptions CreateDateCard(string cardJson)
    {
        var currentDate = DateTime.Now.Date;
        var maxDate = currentDate.AddDays(60);
        var card = AdaptiveCard.FromJson(cardJson).Card;
        var dateInput = card.Body[1] as AdaptiveDateInput;
        dateInput.Min = currentDate.ToString("yyyy-MM-dd");
        dateInput.Max = maxDate.ToString("yyyy-MM-dd");

        var attachment = new Attachment
        {
            ContentType = AdaptiveCard.ContentType,
            Content = JsonConvert.DeserializeObject(card.ToJson())
        };

        return CreatePromptOptions(attachment, dateInput.ErrorMessage);
    }
}
