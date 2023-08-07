﻿namespace DentallApp.Extensions;

public static class ApplicationDependencies
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
                .AddScoped<UserRegisterService>()
                .AddScoped<SpecificTreatmentService>()
                .AddScoped<GeneralTreatmentService>()
                .AddScoped<ProformaInvoiceService>()
                .AddScoped<TokenRefreshService>()
                .AddScoped<EmployeeService>()
                .AddScoped<RoleService>()
                .AddScoped<OfficeService>()
                .AddScoped<AppointmentService>()
                .AddScoped<AppointmentCancellationService>()
                .AddScoped<EmployeeScheduleService>()
                .AddScoped<FavoriteDentistService>()
                .AddScoped<OfficeScheduleService>()
                .AddScoped<AvailabilityService>()
                .AddScoped<ReportDownloadPdfService>()
                .AddScoped<PublicHolidayService>()
                .AddScoped<EmailTemplateService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IOfficeRepository, OfficeRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IAppointmentCancellationRepository, AppointmentCancellationRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddScoped<IFavoriteDentistRepository, FavoriteDentistRepository>()
                .AddScoped<IOfficeScheduleRepository, OfficeScheduleRepository>()
                .AddScoped<IAppointmentReminderQueries, AppointmentReminderQueries>()
                .AddScoped<IPublicHolidayRepository, PublicHolidayRepository>()
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
