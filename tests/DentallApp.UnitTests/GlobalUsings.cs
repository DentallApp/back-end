global using System.Collections;
global using NUnit.Framework;
global using FluentAssertions;
global using AdaptiveCards;
global using Microsoft.Bot.Connector;
global using Microsoft.Bot.Schema;
global using Microsoft.Bot.Builder.Testing;
global using Telerik.JustMock;
global using System.Security.Claims;
global using Newtonsoft.Json.Linq;
global using DotEnv.Core;

global using DentallApp.Features.AvailabilityHours;
global using DentallApp.Features.AvailabilityHours.DTOs;
global using DentallApp.Features.Chatbot.Factories;
global using DentallApp.Features.WeekDays;
global using DentallApp.Features.AppointmentCancellation;
global using DentallApp.Features.AppointmentCancellation.DTOs;
global using DentallApp.Features.SecurityToken;
global using DentallApp.Features.Roles;
global using DentallApp.Features.Appointments;
global using DentallApp.Features.Appointments.DTOs;
global using DentallApp.Features.EmployeeSchedules;
global using DentallApp.Features.EmployeeSchedules.DTOs;
global using DentallApp.Features.GeneralTreatments;
global using DentallApp.Features.GeneralTreatments.DTOs;
global using DentallApp.Features.SpecificTreatments.DTOs;
global using DentallApp.Features.PublicHolidays.Offices;
global using DentallApp.Features.Chatbot;
global using DentallApp.Features.Chatbot.Dialogs;
global using DentallApp.Features.Chatbot.Models;
global using DentallApp.Features.Chatbot.DirectLine;
global using DentallApp.Features.Chatbot.DirectLine.Services;

global using DentallApp.Entities;
global using DentallApp.Helpers.InstantMessaging;
global using DentallApp.Helpers.DateTimeHelpers;
global using DentallApp.Configuration;
global using DentallApp.DataAccess.Repositories;
global using DentallApp.Responses;
global using static DentallApp.Constants.ResponseMessages;

global using static DentallApp.UnitTests.Features.Chatbot.Dialogs.BotServiceMockFactory;
global using static DentallApp.UnitTests.Features.Chatbot.Dialogs.ActivityFactory;

