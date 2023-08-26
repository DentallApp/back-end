namespace DentallApp.Extensions;

public static class ApplicationDependencies
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
                .AddScoped<ProformaInvoiceService>()
                .AddScoped<AppointmentCancellationService>()
                .AddScoped<AvailabilityService>()
                .AddScoped<ReportDownloadPdfService>()
                .AddScoped<EmailTemplateService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IAppointmentCancellationRepository, AppointmentCancellationRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddScoped<IHolidayOfficeRepository, HolidayOfficeRepository>()
                .AddScoped<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddSingleton<IHtmlConverter, HtmlConverterIText>()
                .AddSingleton<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
                .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        return services;
    }
}
