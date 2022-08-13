global using DentallApp.Features.Chatbot;
global using DentallApp.Features.Chatbot.Dialogs;
global using DentallApp.Features.Chatbot.Factories;
global using DentallApp.Features.Chatbot.Helpers;
global using DentallApp.Features.Chatbot.Models;
global using DentallApp.Features.Chatbot.Handlers;
global using DentallApp.Features.GeneralTreatments;
global using DentallApp.Features.GeneralTreatments.DTOs;
global using DentallApp.Features.Persons;
global using DentallApp.Features.Persons.DTOs;
global using DentallApp.Features.Genders;
global using DentallApp.Features.Genders.DTOs;
global using DentallApp.Features.Users;
global using DentallApp.Features.Users.DTOs;
global using DentallApp.Features.Dependents;
global using DentallApp.Features.Dependents.DTOs;
global using DentallApp.Features.Kinships;
global using DentallApp.Features.Kinships.DTOs;
global using DentallApp.Features.UserRegistration;
global using DentallApp.Features.Roles;
global using DentallApp.Features.UserRoles;
global using DentallApp.Features.UserRoles.DTOs;
global using DentallApp.Features.SecurityToken;
global using DentallApp.Features.SecurityToken.DTOs;
global using DentallApp.Features.TokenRefresh;
global using DentallApp.Features.Authentication;
global using DentallApp.Features.Authentication.DTOs;
global using DentallApp.Features.EmailSending;
global using DentallApp.Features.PasswordHasher;
global using DentallApp.Features.EmailVerification;
global using DentallApp.Features.EmailVerification.DTOs;
global using DentallApp.Features.PasswordReset;
global using DentallApp.Features.PasswordReset.DTOs;
global using DentallApp.Features.Employees;
global using DentallApp.Features.Employees.DTOs;
global using DentallApp.Features.Offices;
global using DentallApp.Features.Appoinments;
global using DentallApp.Features.AppoinmentsStatus;

global using DentallApp.Configuration;
global using DentallApp.DataAccess;
global using DentallApp.Models;
global using DentallApp.UnitOfWork;
global using DentallApp.Repositories;
global using DentallApp.Helpers;
global using Response = DentallApp.Helpers.Response;
global using DentallApp.Extensions;
global using DentallApp.Constants;
global using static DentallApp.Constants.ResponseMessages;

global using System;
global using System.Text;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.IdentityModel.Tokens.Jwt;

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
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;


global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using Newtonsoft.Json.Linq;
global using Newtonsoft.Json;

global using DotEnv.Core;

global using SendGrid;
global using SendGrid.Helpers.Mail;
global using SendGrid.Extensions.DependencyInjection;

global using DelegateDecompiler;