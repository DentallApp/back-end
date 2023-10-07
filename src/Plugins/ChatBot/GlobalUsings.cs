global using DentallApp.Features.ChatBot.DirectLine;
global using DentallApp.Features.ChatBot.DirectLine.Services;
global using DentallApp.Features.ChatBot.Dialogs;
global using DentallApp.Features.ChatBot.Factories;
global using DentallApp.Features.ChatBot.Helpers;
global using DentallApp.Features.ChatBot.Models;
global using DentallApp.Features.ChatBot.Handlers;
global using DentallApp.Features.ChatBot.Extensions;
global using DentallApp.Features.ChatBot.Configuration;

global using DentallApp.Shared.Domain;
global using DentallApp.Shared.Appointments;
global using DentallApp.Shared.Constants;
global using static DentallApp.Shared.Constants.ResponseMessages;
global using DentallApp.Shared.Attributes;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Extensions;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Models.Results;
global using DentallApp.Shared.Persistence;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Services;

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

global using Newtonsoft.Json.Linq;
global using Newtonsoft.Json;
global using DotEnv.Core;