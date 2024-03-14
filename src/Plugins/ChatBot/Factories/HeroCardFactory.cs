namespace Plugin.ChatBot.Factories;
using Attachment = Microsoft.Bot.Schema.Attachment;

public class HeroCardFactory
{
    private static HeroCard CreateHeroCard(string title = null, string text = null)
        => new()
        {
            Title   = title,
            Text    = text,
            Buttons = new List<CardAction>()
        };

    private static CardAction CreateCardAction(AvailableTimeRangeResponse availableHour)
    {
        var str = availableHour.ToString();
        return new()
        {
            Title = str,
            Text  = str,
            Value = str,
            Type  = ActionTypes.MessageBack
        };
    }

    public static List<HeroCard> CreateSchedulesHeroCard(List<AvailableTimeRangeResponse> availableHours)
    {
        int totalButtons        = 5;
        int totalAvailableHours = availableHours.Count;
        int totalHeroCards      = availableHours.Count / totalButtons;
        var heroCards           = new List<HeroCard>();
        int heroCardIndex       = 0;
        int availableHourIndex  = 0;
        int cardActionIndex;

        // This creates multiple hero cards and each subiteration adds five buttons to the hero card.
        while (heroCardIndex++ < totalHeroCards)
        {
            var heroCard = CreateHeroCard();
            heroCards.Add(heroCard);
            cardActionIndex = 0;
            while (cardActionIndex++ < totalButtons)
            {
                var availableHour = availableHours[availableHourIndex++];
                heroCard.Buttons.Add(CreateCardAction(availableHour));
            }
        }

        // This condition checks whether it is necessary to add a last hero card.
        // This ensures that the missing available hours are on any hero card.
        if (availableHourIndex < totalAvailableHours)
        {
            var heroCard = CreateHeroCard();
            heroCards.Add(heroCard);
            while (availableHourIndex < totalAvailableHours)
            {
                var availableHour = availableHours[availableHourIndex++];
                heroCard.Buttons.Add(CreateCardAction(availableHour));
            }
        }
        return heroCards;
    }

    public static PromptOptions CreateSchedulesCarousel(List<AvailableTimeRangeResponse> availableHours)
    {
        var heroCards   = CreateSchedulesHeroCard(availableHours);
        var attachments = new List<Attachment>();
        heroCards.ForEach(heroCard => attachments.Add(heroCard.ToAttachment()));
        var reply = MessageFactory.Attachment(attachments);
        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        return new PromptOptions
        {
            Prompt = reply as Activity,
            RetryPrompt = MessageFactory.Text(Messages.SelectSchedule)
        };
    }
}
