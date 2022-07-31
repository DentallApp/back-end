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
                .AddTransient<IGeneralTreatmentService, GeneralTreatmentService>()
                .AddTransient<IPasswordResetService, PasswordResetService>()
                .AddTransient<IDependentService, DependentService>()
                .AddTransient<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IKinshipRepository, KinshipRepository>()
                .AddTransient<IGenderRepository, GenderRepository>()
                .AddTransient<IGeneralTreatmentRepository, GeneralTreatmentRepository>();

        return services;
    }
}
