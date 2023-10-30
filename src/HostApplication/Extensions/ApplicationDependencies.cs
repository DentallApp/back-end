namespace DentallApp.HostApplication.Extensions;

public static class ApplicationDependencies
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IEntityService<>), typeof(EntityService<>))
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IEmailService, EmailService>()
            .AddSingleton<IDateTimeService, DateTimeService>()
            .AddSingleton<IHtmlConverter, HtmlConverterIText>()
            .AddSingleton<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
            .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
            .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISchedulingQueries, SchedulingQueries>()
            .AddScoped<IAvailabilityQueries, AvailabilityQueries>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ITreatmentRepository, TreatmentRepository>()
            .AddScoped<IAppointmentRepository, AppointmentRepository>();

        return services;
    }
}
