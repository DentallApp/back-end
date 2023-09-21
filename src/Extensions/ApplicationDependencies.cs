using DentallApp.Features.Dependents.UseCases;

namespace DentallApp.Extensions;

public static class ApplicationDependencies
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
                .AddScoped(typeof(IEntityService<>), typeof(EntityService<>))
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddScoped<IOfficeHolidayRepository, OfficeHolidayRepository>()
                .AddScoped<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddSingleton<IHtmlConverter, HtmlConverterIText>()
                .AddSingleton<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
                .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
                .AddSingleton<IDateTimeService, DateTimeService>()
                .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Specifies the assembly for Scrutor to search for types.
        var assembly = typeof(GetDependentsByUserIdUseCase).Assembly;

        services.Scan(scan => scan
        // Search the types from the specified assemblies.
            .FromAssemblies(assembly)
              // Registers the concrete classes as a service, for example: 'CreateUserUseCase'.
              // It also registers interfaces as a service, for example: 'ICreateUserUseCase'.
              .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
                .AsSelfWithInterfaces()
                .WithScopedLifetime()); 

        return services;
    }
}
