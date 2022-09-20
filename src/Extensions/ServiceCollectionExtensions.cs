namespace DentallApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUserRegisterService, UserRegisterService>()
                .AddTransient<IEmailVerificationService, EmailVerificationService>()
                .AddTransient<IEmailTemplateService, EmailTemplateService>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<ISpecificTreatmentService, SpecificTreatmentService>()
                .AddTransient<IGeneralTreatmentService, GeneralTreatmentService>()
                .AddTransient<IProformaInvoiceService, ProformaInvoiceService>()
                .AddTransient<IPasswordResetService, PasswordResetService>()
                .AddTransient<IDependentService, DependentService>()
                .AddTransient<ITokenRefreshService, TokenRefreshService>()
                .AddTransient<IEmployeeService, EmployeeService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IOfficeService, OfficeService>()
                .AddScoped<IAppoinmentService, AppoinmentService>()
                .AddTransient<IEmployeeScheduleService, EmployeeScheduleService>()
                .AddTransient<IFavoriteDentistService, FavoriteDentistService>()
                .AddTransient<IOfficeScheduleService, OfficeScheduleService>()
                .AddScoped<IAvailabilityService, AvailabilityService>()
                .AddTransient<IPersonService, PersonService>()
                .AddTransient<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IKinshipRepository, KinshipRepository>()
                .AddTransient<IGenderRepository, GenderRepository>()
                .AddTransient<IAppoinmentStatusRepository, AppoinmentStatusRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IOfficeRepository, OfficeRepository>()
                .AddScoped<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddTransient<IAppoinmentRepository, AppoinmentRepository>()
                .AddScoped<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddTransient<IFavoriteDentistRepository, FavoriteDentistRepository>()
                .AddTransient<IOfficeScheduleRepository, OfficeScheduleRepository>()
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddScoped<IAppoinmentReminderRepository, AppoinmentReminderRepository>()
                .AddTransient<IReportQuery, ReportQuery>()
                .AddTransient<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddTransient<IHtmlConverter, HtmlConverterIText>()
                .AddTransient<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>()
                .AddTransient<IPasswordHasher, PasswordHasherBcrypt>()
                .AddSingleton<IInstantMessaging, WhatsAppMessaging>();

        return services;
    }
}
