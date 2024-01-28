namespace DentallApp.Infrastructure;

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
            .AddScoped<IEmailService, EmailService>()
            .AddSingleton<IDateTimeService, DateTimeService>()
            .AddSingleton<IHtmlConverter, HtmlConverterIText>()
            .AddSingleton<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
            .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
            .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        return services;
    }
}
