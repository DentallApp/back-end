global using System.Collections;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using NUnit.Framework;
global using FluentAssertions;
global using FluentValidation;
global using FluentValidation.TestHelper;
global using Microsoft.Bot.Schema;
global using Telerik.JustMock;
global using DotEnv.Core;
global using SimpleResults;

global using DentallApp.Core.Appointments.UseCases;
global using DentallApp.Core.Appointments.UseCases.GetAvailableHours;
global using DentallApp.Infrastructure.Services;

global using DentallApp.Shared;
global using DentallApp.Shared.Entities;
global using DentallApp.Shared.Entities.EmployeeSchedules;
global using DentallApp.Shared.Entities.WeekDays;
global using DentallApp.Shared.Interfaces;
global using DentallApp.Shared.Interfaces.Appointments;
global using DentallApp.Shared.Interfaces.Persistence.Repositories;
global using DentallApp.Shared.Interfaces.Persistence;
global using DentallApp.Shared.Resources.Weekdays;
global using DentallApp.Shared.Resources.ApiResponses;
global using DentallApp.Shared.Reasons;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.ValidationRules;

global using Plugin.ChatBot.Factories;
global using Plugin.ChatBot.Models;
global using Plugin.ChatBot.DirectLine;
global using Plugin.ChatBot.DirectLine.Services;
global using Plugin.IdentityDocument.Ecuador;
