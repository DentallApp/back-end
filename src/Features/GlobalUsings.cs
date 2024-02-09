global using DentallApp.Features.Appointments.UseCases;
global using DentallApp.Features.Appointments.UseCases.GetAvailableHours;

global using DentallApp.Shared.Domain;
global using DentallApp.Shared.Domain.EmployeeSchedules;
global using DentallApp.Shared.Interfaces;
global using DentallApp.Shared.Interfaces.Appointments;
global using DentallApp.Shared.Interfaces.Persistence;
global using DentallApp.Shared.Interfaces.Persistence.Repositories;
global using DentallApp.Shared.Constants;
global using DentallApp.Shared.Resources.ApiResponses;
global using DentallApp.Shared.Resources.Kinships;
global using DentallApp.Shared.Reasons;
global using DentallApp.Shared.Attributes;
global using DentallApp.Shared.Configuration;
global using DentallApp.Shared.Extensions;
global using DentallApp.Shared.Models;
global using DentallApp.Shared.Models.Claims;

global using System.Text.Json.Serialization;
global using System.Data;
global using System.Security.Claims;
global using System.ComponentModel.DataAnnotations;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;

global using Microsoft.EntityFrameworkCore;

global using SimpleResults;
global using YeSql.Net;
global using Dapper;
