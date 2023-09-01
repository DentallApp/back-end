namespace DentallApp.UnitTests.Features.Chatbot.Factories;

public class HeroCardFactoryTests
{
    [Test]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsMultiplesOfFive_ShouldReturnsHeroCardsWithAvailableHours()
    {
        // Arrange
        const string type = ActionTypes.MessageBack;
        var availableHours = new List<AvailableTimeRangeResponse>
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
        var expectedHeroCards = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Title = "07:00 - 08:00", Text = "07:00 - 08:00", Value = "07:00 - 08:00", Type = type },
                    new() { Title = "08:00 - 09:00", Text = "08:00 - 09:00", Value = "08:00 - 09:00", Type = type },
                    new() { Title = "09:00 - 10:00", Text = "09:00 - 10:00", Value = "09:00 - 10:00", Type = type },
                    new() { Title = "10:00 - 11:00", Text = "10:00 - 11:00", Value = "10:00 - 11:00", Type = type },
                    new() { Title = "11:00 - 12:00", Text = "11:00 - 12:00", Value = "11:00 - 12:00", Type = type }
                }
            },
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Title = "12:00 - 13:00", Text = "12:00 - 13:00", Value = "12:00 - 13:00", Type = type },
                    new() { Title = "13:00 - 14:00", Text = "13:00 - 14:00", Value = "13:00 - 14:00", Type = type },
                    new() { Title = "15:00 - 16:00", Text = "15:00 - 16:00", Value = "15:00 - 16:00", Type = type },
                    new() { Title = "16:00 - 16:30", Text = "16:00 - 16:30", Value = "16:00 - 16:30", Type = type },
                    new() { Title = "17:00 - 17:30", Text = "17:00 - 17:30", Value = "17:00 - 17:30", Type = type }
                }
            }
        };

        // Act
        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        // Assert
        heroCards.Should().BeEquivalentTo(expectedHeroCards);
    }

    [Test]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsNotMultiplesOfFive_ShouldReturnsHeroCardsWithAvailableHours()
    {
        // Arrange
        const string type = ActionTypes.MessageBack;
        var availableHours = new List<AvailableTimeRangeResponse>
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
        var expectedHeroCards = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Title = "07:00 - 08:00", Text = "07:00 - 08:00", Value = "07:00 - 08:00", Type = type },
                    new() { Title = "08:00 - 09:00", Text = "08:00 - 09:00", Value = "08:00 - 09:00", Type = type },
                    new() { Title = "09:00 - 10:00", Text = "09:00 - 10:00", Value = "09:00 - 10:00", Type = type },
                    new() { Title = "10:00 - 11:00", Text = "10:00 - 11:00", Value = "10:00 - 11:00", Type = type },
                    new() { Title = "11:00 - 12:00", Text = "11:00 - 12:00", Value = "11:00 - 12:00", Type = type }
                }
            },
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Title = "12:00 - 13:00", Text = "12:00 - 13:00", Value = "12:00 - 13:00", Type = type },
                    new() { Title = "13:00 - 14:00", Text = "13:00 - 14:00", Value = "13:00 - 14:00", Type = type },
                    new() { Title = "15:00 - 16:00", Text = "15:00 - 16:00", Value = "15:00 - 16:00", Type = type }
                }
            }
        };

        // Act
        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        // Assert
        heroCards.Should().BeEquivalentTo(expectedHeroCards);
    }

    [Test]
    public void CreateSchedulesHeroCard_WhenNumberOfAvailableHoursIsLessThanFive_ShouldReturnsHeroCardsWithAvailableHours()
    {
        // Arrange
        const string type = ActionTypes.MessageBack;
        var availableHours = new List<AvailableTimeRangeResponse>
        {
            new() { StartHour = "07:00",  EndHour = "08:00" }
        };
        var expectedHeroCards = new List<HeroCard>
        {
            new HeroCard()
            {
                Buttons = new List<CardAction>
                {
                    new() { Title = "07:00 - 08:00", Text = "07:00 - 08:00", Value = "07:00 - 08:00", Type = type }
                }
            }
        };

        // Act
        var heroCards = HeroCardFactory.CreateSchedulesHeroCard(availableHours);

        // Assert
        heroCards.Should().BeEquivalentTo(expectedHeroCards);
    }
}
