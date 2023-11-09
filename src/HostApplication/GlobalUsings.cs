global using DentallApp.Features.Dependents.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;

global using DentallApp.HostApplication;
global using DentallApp.HostApplication.Middlewares;
global using DentallApp.HostApplication.Extensions;

global using DentallApp.Infrastructure.Services;
global using DentallApp.Infrastructure.Services.TokenProvider;
global using DentallApp.Infrastructure.Persistence;
global using DentallApp.Infrastructure.Persistence.Repositories;

global using DentallApp.Shared.Appointments;
global using static DentallApp.Shared.Constants.ResponseMessages;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Persistence;
global using DentallApp.Shared.Persistence.Repositories;
global using DentallApp.Shared.Services;
global using DentallApp.Shared.Plugin.Contracts;

global using System.Runtime.Loader;
global using System.Reflection;
global using System.Net;
global using System.Data;
global using System.Text;
global using System.Text.Encodings.Web;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
global using MySqlConnector;
global using CPlugin.Net;
