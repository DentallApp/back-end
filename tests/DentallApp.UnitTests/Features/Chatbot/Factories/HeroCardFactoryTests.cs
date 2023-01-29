namespace DentallApp.UnitTests.Features.Chatbot.Factories;

[TestClass]
public class HeroCardFactoryTests
{
    [TestMethod]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsMultiplesOfFive_ShouldReturnHeroCardsWithAvailableHours()
    {
        var availableHours = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "07:00",  EndHour = "08:00" },
            new() { StartHour = "08:00",  EndHour = "09:00" },
            new() { StartHour = "09:00",  EndHour = "10:00" },
            new() { StartHour = "10:00",  EndHour = "11:00" },
            new() { StartHour = "11:00",  EndHour = "12:00" },
            new() { StartHour = "12:00",  EndHour = "13:00" },
            new() { StartHour = "13:00",  EndHour = "14:00" },
            new() { StartHour = "15:00",  EndHour = "16:00" },
            new() { StartHour = "16:00",  EndHour = "16:30" },
            new() { StartHour = "17:00",  EndHour = "17:30" }
        };
        var heroCardsExpected = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Value = "07:00 - 08:00" },
                    new() { Value = "08:00 - 09:00" },
                    new() { Value = "09:00 - 10:00" },
                    new() { Value = "10:00 - 11:00" },
                    new() { Value = "11:00 - 12:00" }
                }
            },
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Value = "12:00 - 13:00" },
                    new() { Value = "13:00 - 14:00" },
                    new() { Value = "15:00 - 16:00" },
                    new() { Value = "16:00 - 16:30" },
                    new() { Value = "17:00 - 17:30" }
                }
            }
        };

        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        Assert.AreEqual(heroCardsExpected.Count, actual: heroCards.Count);
        for (int i = 0; i < heroCards.Count; i++)
        {
            var heroCard = heroCards[i];
            var heroCardExpected = heroCardsExpected[i];
            Assert.AreEqual(heroCardExpected.Buttons.Count, actual: heroCard.Buttons.Count);
            for (int j = 0; j < heroCard.Buttons.Count; j++)
                Assert.AreEqual(heroCardExpected.Buttons[j].Value, actual: heroCard.Buttons[j].Value);
        }
    }

    [TestMethod]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsNotMultiplesOfFive_ShouldReturnHeroCardsWithAvailableHours()
    {
        var availableHours = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "07:00",  EndHour = "08:00" },
            new() { StartHour = "08:00",  EndHour = "09:00" },
            new() { StartHour = "09:00",  EndHour = "10:00" },
            new() { StartHour = "10:00",  EndHour = "11:00" },
            new() { StartHour = "11:00",  EndHour = "12:00" },
            new() { StartHour = "12:00",  EndHour = "13:00" },
            new() { StartHour = "13:00",  EndHour = "14:00" },
            new() { StartHour = "15:00",  EndHour = "16:00" }
        };
        var heroCardsExpected = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Value = "07:00 - 08:00" },
                    new() { Value = "08:00 - 09:00" },
                    new() { Value = "09:00 - 10:00" },
                    new() { Value = "10:00 - 11:00" },
                    new() { Value = "11:00 - 12:00" }
                }
            },
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Value = "12:00 - 13:00" },
                    new() { Value = "13:00 - 14:00" },
                    new() { Value = "15:00 - 16:00" }
                }
            }
        };

        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        Assert.AreEqual(heroCardsExpected.Count, actual: heroCards.Count);
        for (int i = 0; i < heroCards.Count; i++)
        {
            var heroCard = heroCards[i];
            var heroCardExpected = heroCardsExpected[i];
            Assert.AreEqual(heroCardExpected.Buttons.Count, actual: heroCard.Buttons.Count);
            for (int j = 0; j < heroCard.Buttons.Count; j++)
                Assert.AreEqual(heroCardExpected.Buttons[j].Value, actual: heroCard.Buttons[j].Value);
        }
    }

    [TestMethod]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsLessThanFive_ShouldReturnHeroCardWithAvailableHours()
    {
        var availableHours = new List<AvailableTimeRangeDto>
        {
            new() { StartHour = "07:00",  EndHour = "08:00" }
        };
        var heroCardsExpected = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Value = "07:00 - 08:00" }
                }
            }
        };

        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        Assert.AreEqual(heroCardsExpected.Count, actual: heroCards.Count);
        var heroCard = heroCards[0];
        var heroCardExpected = heroCardsExpected[0];
        Assert.AreEqual(heroCardExpected.Buttons.Count, actual: heroCard.Buttons.Count);
        Assert.AreEqual(heroCardExpected.Buttons[0].Value, actual: heroCard.Buttons[0].Value);
    }
}
