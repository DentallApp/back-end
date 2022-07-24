namespace DentallApp.Features.EmailVerification;

public interface IEmailVerificationService
{
    Task<Response<UserLoginDto>> VerifyEmailAsync(string token);
}
