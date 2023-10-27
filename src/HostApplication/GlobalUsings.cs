global using DentallApp.Features.Dependents.UseCases;
global using DentallApp.Features.ChatBot.Extensions;
global using DentallApp.Features.AppointmentReminders;

global using DentallApp.HostApplication.Middlewares;
global using DentallApp.HostApplication.Extensions;

global using DentallApp.Infrastructure.Services;
global using DentallApp.Infrastructure.Services.TokenProvider;
global using DentallApp.Infrastructure.Persistence;
global using DentallApp.Infrastructure.Persistence.Repositories;

global using DentallApp.Shared.Appointments;
global using static DentallApp.Shared.Constants.ResponseMessages;
global using DentallApp.Shared.Configuration;
global using Response = DentallApp.Shared.Models.Results.Response;
global using DentallApp.Shared.Persistence;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Services;

global using System.Net;
global using System.Data;
global using System.Text;
global using System.Text.Encodings.Web;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Diagnostics;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.IdentityModel.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;

global using DotEnv.Core;
global using SimpleResults;
global using SendGrid.Extensions.DependencyInjection;

global using EntityFramework.Exceptions.Common;
global using Quartz;
global using MySqlConnector;
