namespace DentallApp.Extensions;

public static class ServiceCollectionExtensions
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
                .AddScoped<IAppoinmentService, AppoinmentService>()
                .AddScoped<IEmployeeScheduleService, EmployeeScheduleService>()
                .AddScoped<IFavoriteDentistService, FavoriteDentistService>()
                .AddScoped<IOfficeScheduleService, OfficeScheduleService>()
                .AddScoped<IAvailabilityService, AvailabilityService>()
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IReportDownloadPdfService, ReportDownloadPdfService>()
                .AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IKinshipRepository, KinshipRepository>()
                .AddScoped<IGenderRepository, GenderRepository>()
                .AddScoped<IAppoinmentStatusRepository, AppoinmentStatusRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IOfficeRepository, OfficeRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddScoped<IAppoinmentRepository, AppoinmentRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddScoped<IFavoriteDentistRepository, FavoriteDentistRepository>()
                .AddScoped<IOfficeScheduleRepository, OfficeScheduleRepository>()
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<IAppoinmentReminderRepository, AppoinmentReminderRepository>()
                .AddScoped<IReportQuery, ReportQuery>()
                .AddScoped<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddTransient<IHtmlConverter, HtmlConverterIText>()
                .AddTransient<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
                .AddTransient<IPasswordHasher, PasswordHasherBcrypt>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        return services;
    }
}
