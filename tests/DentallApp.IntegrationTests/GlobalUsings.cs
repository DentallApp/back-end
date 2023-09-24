global using NUnit.Framework;
global using FluentAssertions;
global using Telerik.JustMock;
global using AdaptiveCards;
global using Microsoft.Bot.Connector;
global using Microsoft.Bot.Builder.Testing;
global using Microsoft.Bot.Schema;
global using Newtonsoft.Json.Linq;

global using static DentallApp.IntegrationTests.Features.Chatbot.Dialogs.BotServiceMockFactory;
global using static DentallApp.IntegrationTests.Features.Chatbot.Dialogs.ActivityFactory;

global using DentallApp.Features.Chatbot;
global using DentallApp.Features.Chatbot.Dialogs;
global using DentallApp.Features.Appointments.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;

global using DentallApp.Domain.ValueObjects;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Models.Results;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Services;
global using static DentallApp.Shared.Constants.ResponseMessages;