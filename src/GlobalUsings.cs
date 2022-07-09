global using DentallApp.Features.Chatbot;


global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.IO;
global using System.Linq;
global using System.Reflection;

global using Microsoft.Bot.Builder;
global using Microsoft.Bot.Builder.Integration.AspNet.Core;
global using Microsoft.Bot.Builder.Dialogs;
global using Microsoft.Bot.Builder.Dialogs.Choices;
global using Microsoft.Bot.Builder.TraceExtensions;
global using Microsoft.Bot.Schema;
global using Microsoft.Bot.Connector.Authentication;
global using AdaptiveCards;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;

global using Microsoft.OpenApi.Models;

global using Newtonsoft.Json.Linq;
global using Newtonsoft.Json;