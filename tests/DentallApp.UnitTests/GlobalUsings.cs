global using System.Collections;
global using NUnit.Framework;
global using FluentAssertions;
global using Microsoft.Bot.Schema;
global using Telerik.JustMock;
global using System.Security.Claims;
global using DotEnv.Core;

global using DentallApp.Features.Appointments.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;
global using DentallApp.Features.Chatbot.Factories;
global using DentallApp.Features.Chatbot.Models;
global using DentallApp.Features.Chatbot.DirectLine;
global using DentallApp.Features.Chatbot.DirectLine.Services;

global using DentallApp.Domain;
global using DentallApp.Infrastructure.Services;

global using DentallApp.Shared;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Constants;
global using DentallApp.Shared.Services;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Persistence;
global using static DentallApp.Shared.Constants.ResponseMessages;
