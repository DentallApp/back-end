﻿namespace DentallApp.Infrastructure;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISchedulingQueries, SchedulingQueries>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ITreatmentRepository, TreatmentRepository>()
            .AddScoped<IAppointmentRepository, AppointmentRepository>();

        services
            .AddScoped(typeof(IEntityService<>), typeof(EntityService<>))
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<ICurrentUser, CurrentUserService>()
            .AddScoped<ICurrentEmployee, CurrentEmployeeService>()
            .AddSingleton<IDateTimeService, DateTimeService>()
            .AddSingleton<IHtmlConverter, HtmlConverterIText>()
            .AddSingleton<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
            .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
            .AddSingleton<IFileTypeValidator, FileTypeService>();

        services.AddScoped(serviceProvider =>
        {
            var httpContextAccessor = serviceProvider
                .GetService<IHttpContextAccessor>();

            ClaimsPrincipal user = httpContextAccessor
                .HttpContext
                .User;

            return user;
        });

        return services;
    }
}
