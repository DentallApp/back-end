namespace DentallApp.Features.PasswordReset;

public class PasswordResetService : IPasswordResetService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;

    public PasswordResetService(IUserRepository userRepository, 
                                ITokenService tokenService, 
                                IPasswordHasher passwordHasher,
                                IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
    }

    public async Task<Response> ResetPasswordAsync(string token, string newPassword)
    {
        var claimIdentity = _tokenService.GetClaimsIdentity(token);
        if (claimIdentity is null)
            return new Response(PasswordResetTokenInvalidMessage);

        if (!claimIdentity.HasClaim(CustomClaimsType.UserId))
            return new Response(string.Format(MissingClaimMessage, CustomClaimsType.UserId));

        var user = await _userRepository.GetByIdAsync(claimIdentity.GetUserId());
        if (user is null)
            return new Response(UsernameNotFoundMessage);

        var claimPrincipal = _tokenService.ValidatePasswordResetToken(token, user.Password);
        if (claimPrincipal is null)
            return new Response(PasswordResetTokenInvalidMessage);

        user.Password = _passwordHasher.HashPassword(newPassword);
        await _userRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = PasswordSuccessfullyResetMessage
        };
    }

    public async Task<Response> SendPasswordResetLinkAsync(string email)
    {
        var user = await _userRepository.GetUserForResetPassword(email);
        if (user is null)
            return new Response(UsernameNotFoundMessage);

        var passwordResetToken = _tokenService.CreatePasswordResetToken(user.UserId, user.UserName, user.Password);
        await _emailService.SendEmailForResetPasswordAsync(user.UserName, user.Name, passwordResetToken);

        return new Response
        {
            Success = true,
            Message = SendPasswordResetLinkMessage
        };
    }
}
