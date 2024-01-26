global using System.Collections;
global using NUnit.Framework;
global using FluentAssertions;
global using Microsoft.Bot.Schema;
global using Telerik.JustMock;
global using System.Security.Claims;
global using DotEnv.Core;

global using DentallApp.Features.Appointments.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;
global using DentallApp.Features.ChatBot.Factories;
global using DentallApp.Features.ChatBot.Models;
global using DentallApp.Features.ChatBot.DirectLine;
global using DentallApp.Features.ChatBot.DirectLine.Services;

global using DentallApp.Infrastructure.Services;

global using DentallApp.Shared.Domain;
global using DentallApp.Shared.Appointments;
global using DentallApp.Shared.Resources.Weekdays;
global using DentallApp.Shared.Resources.ApiResponses;
global using DentallApp.Shared.Constants;
global using DentallApp.Shared.Services;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Persistence;
