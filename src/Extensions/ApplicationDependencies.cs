namespace DentallApp.Extensions;

public static class ApplicationDependencies
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRegisterService, UserRegisterService>()
                .AddScoped<IEmailVerificationService, EmailVerificationService>()
                .AddScoped<IEmailTemplateService, EmailTemplateService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<ISpecificTreatmentService, SpecificTreatmentService>()
                .AddScoped<IGeneralTreatmentService, GeneralTreatmentService>()
                .AddScoped<IProformaInvoiceService, ProformaInvoiceService>()
                .AddScoped<IPasswordResetService, PasswordResetService>()
                .AddScoped<IDependentService, DependentService>()
                .AddScoped<ITokenRefreshService, TokenRefreshService>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IOfficeService, OfficeService>()
                .AddScoped<IAppointmentService, AppointmentService>()
                .AddScoped<IAppointmentCancellationService, AppointmentCancellationService>()
                .AddScoped<IEmployeeScheduleService, EmployeeScheduleService>()
                .AddScoped<IFavoriteDentistService, FavoriteDentistService>()
                .AddScoped<IOfficeScheduleService, OfficeScheduleService>()
                .AddScoped<IAvailabilityService, AvailabilityService>()
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IReportDownloadPdfService, ReportDownloadPdfService>()
                .AddScoped<IPublicHolidayService, PublicHolidayService>()
                .AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IDependentRepository, DependentRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IKinshipRepository, KinshipRepository>()
                .AddScoped<IGenderRepository, GenderRepository>()
                .AddScoped<IAppointmentStatusRepository, AppointmentStatusRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IOfficeRepository, OfficeRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IAppointmentCancellationRepository, AppointmentCancellationRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddScoped<IFavoriteDentistRepository, FavoriteDentistRepository>()
                .AddScoped<IOfficeScheduleRepository, OfficeScheduleRepository>()
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<IAppointmentReminderRepository, AppointmentReminderRepository>()
                .AddScoped<IReportQuery, ReportQuery>()
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
