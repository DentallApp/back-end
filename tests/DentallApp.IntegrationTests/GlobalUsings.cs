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
global using DentallApp.Features.Chatbot.Models;
global using DentallApp.Features.Chatbot.Dialogs;
global using DentallApp.Features.Appointments.DTOs;
global using DentallApp.Features.SpecificTreatments.DTOs;
global using DentallApp.Features.AvailabilityHours.DTOs;

global using DentallApp.Responses;
global using DentallApp.Configuration;
global using DentallApp.Helpers.DateTimeHelpers;
global using static DentallApp.Constants.ResponseMessages;