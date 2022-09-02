namespace DentallApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>()
                .AddTransient<IPasswordHasher, PasswordHasherBcrypt>()
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
                .AddTransient<IAppoinmentService, AppoinmentService>()
                .AddTransient<IEmployeeScheduleService, EmployeeScheduleService>()
                .AddTransient<IFavoriteDentistService, FavoriteDentistService>()
                .AddTransient<IOfficeScheduleService, OfficeScheduleService>()
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
                .AddTransient<ISpecificTreatmentRepository, SpecificTreatmentRepository>()
                .AddTransient<IAppoinmentRepository, AppoinmentRepository>()
                .AddTransient<IEmployeeScheduleRepository, EmployeeScheduleRepository>()
                .AddTransient<IFavoriteDentistRepository, FavoriteDentistRepository>()
                .AddTransient<IOfficeScheduleRepository, OfficeScheduleRepository>()
                .AddTransient<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddTransient<IHtmlConverter, HtmlConverterIText>()
                .AddTransient<IHtmlTemplateLoader, HtmlTemplateLoaderScriban>();

        return services;
    }
}
