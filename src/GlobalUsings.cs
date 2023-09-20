global using DentallApp.Features.Chatbot;
global using DentallApp.Features.Chatbot.DirectLine;
global using DentallApp.Features.Chatbot.DirectLine.Services;
global using DentallApp.Features.Chatbot.Dialogs;
global using DentallApp.Features.Chatbot.Factories;
global using DentallApp.Features.Chatbot.Helpers;
global using DentallApp.Features.Chatbot.Models;
global using DentallApp.Features.Chatbot.Handlers;
global using DentallApp.Features.Chatbot.Extensions;
global using DentallApp.Features.Chatbot.Configuration;
global using DentallApp.Features.Appointments.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;
global using DentallApp.Features.AppointmentReminders;

global using DentallApp.Middlewares;
global using DentallApp.Extensions;

global using DentallApp.Domain;
global using DentallApp.Domain.Entities;
global using DentallApp.Domain.Shared;

global using DentallApp.Infrastructure.Services;
global using DentallApp.Infrastructure.Services.TokenProvider;
global using DentallApp.Infrastructure.Persistence;
global using DentallApp.Infrastructure.Persistence.Repositories;
global using DentallApp.Infrastructure.Persistence.Extensions;
global using DentallApp.Infrastructure.Persistence.SeedsData;

global using DentallApp.Shared.Constants;
global using static DentallApp.Shared.Constants.ResponseMessages;
global using static DentallApp.Shared.Constants.MessageTemplates;
global using DentallApp.Shared;
global using DentallApp.Shared.Attributes;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Extensions;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Models.Claims;
global using DentallApp.Shared.Models.Results;
global using Response = DentallApp.Shared.Models.Results.Response;
global using DentallApp.Shared.Persistence;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Services;

global using System.Globalization;
global using System.Net;
global using System.Data;
global using System.Text;
global using System.Net.Mime;
global using System.Text.Encodings.Web;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;

global using Microsoft.Bot.Builder;
global using Microsoft.Bot.Builder.Integration.AspNet.Core;
global using Microsoft.Bot.Builder.Dialogs;
global using Microsoft.Bot.Builder.TraceExtensions;
global using Microsoft.Bot.Schema;
global using Microsoft.Bot.Connector.Authentication;
global using AdaptiveCards;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.IdentityModel.Logging;
global using Microsoft.OpenApi.Models;
global using Microsoft.Extensions.Options;

global using Newtonsoft.Json.Linq;
global using Newtonsoft.Json;

global using DotEnv.Core;
global using Dapper;

global using SendGrid;
global using SendGrid.Helpers.Mail;
global using SendGrid.Extensions.DependencyInjection;

global using EntityFramework.Exceptions.Common;
global using EntityFramework.Exceptions.MySQL.Pomelo;
global using DelegateDecompiler;
global using FileTypeChecker;
global using FileTypeChecker.Extensions;
global using iText.Html2pdf;
global using Scriban;
global using Quartz;
global using MySqlConnector;
