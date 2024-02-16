global using Plugin.ChatBot;
global using Plugin.ChatBot.DirectLine;
global using Plugin.ChatBot.DirectLine.Services;
global using Plugin.ChatBot.Dialogs;
global using Plugin.ChatBot.Factories;
global using Plugin.ChatBot.Helpers;
global using Plugin.ChatBot.Models;
global using Plugin.ChatBot.Handlers;
global using Plugin.ChatBot.Extensions;
global using Plugin.ChatBot.Configuration;

global using DentallApp.Shared.Entities.WeekDays;
global using DentallApp.Shared.Entities.EmployeeSchedules;
global using DentallApp.Shared.ValueObjects;
global using DentallApp.Shared.Interfaces;
global using DentallApp.Shared.Interfaces.Appointments;
global using DentallApp.Shared.Interfaces.Persistence;
global using DentallApp.Shared.Interfaces.Persistence.Repositories;
global using DentallApp.Shared.Constants;
global using DentallApp.Shared.Resources.ApiResponses;
global using DentallApp.Shared.Reasons;
global using DentallApp.Shared.Attributes;
global using DentallApp.Shared.Extensions;
global using DentallApp.Shared.Models;

global using System.Data;
global using System.Text;
global using System.Net.Mime;

global using Microsoft.Bot.Builder;
global using Microsoft.Bot.Builder.Dialogs;
global using Microsoft.Bot.Builder.TraceExtensions;
global using Microsoft.Bot.Schema;
global using Microsoft.Bot.Connector.Authentication;
global using AdaptiveCards;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Bot.Builder.Integration.AspNet.Core;
global using Microsoft.EntityFrameworkCore;

global using Newtonsoft.Json.Linq;
global using Newtonsoft.Json;
global using DotEnv.Core;
global using SimpleResults;
global using CPlugin.Net;
