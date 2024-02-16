global using System.Globalization;
global using NUnit.Framework;
global using FluentAssertions;
global using Telerik.JustMock;
global using AdaptiveCards;
global using Microsoft.Bot.Connector;
global using Microsoft.Bot.Builder.Testing;
global using Microsoft.Bot.Schema;
global using Newtonsoft.Json.Linq;
global using SimpleResults;

global using DentallApp.Shared.Interfaces;
global using DentallApp.Shared.Interfaces.Appointments;
global using DentallApp.Shared.ValueObjects;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Resources.ApiResponses;
global using DentallApp.Shared.Reasons;

global using Plugin.ChatBot.Dialogs;
global using Plugin.ChatBot.Configuration;
global using static Plugin.ChatBot.IntegrationTests.Dialogs.BotServiceMockFactory;
global using static Plugin.ChatBot.IntegrationTests.Dialogs.ActivityFactory;
