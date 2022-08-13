namespace DentallApp.Features.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, 
                       ITokenService tokenService,
                       IPasswordHasher passwordHasher,
                       IEmployeeRepository employeeRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _employeeRepository = employeeRepository;
    }

    public async Task<Response> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetFullUserProfileAsync(username);
        if (user is null || !_passwordHasher.Verify(text: password, passwordHash: user.Password))
            return new Response(EmailOrPasswordIncorrectMessage);

        if (user.IsUnverified())
            return new Response(EmailNotConfirmedMessage);

        user.RefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = _tokenService.CreateExpiryForRefreshToken();
        await _userRepository.SaveAsync();

        if (user.IsBasicUser())
        {
            var userLoginDto = user.MapToUserLoginDto();
            userLoginDto.AccessToken = _tokenService.CreateAccessToken(userLoginDto.MapToUserClaims());
            userLoginDto.RefreshToken = user.RefreshToken;
            return new Response
            {
                Success = true,
                Data = userLoginDto,
                Message = SuccessfulLoginMessage
            };
        }

        var employee = await _employeeRepository.GetEmployeeByUserId(user.Id);
        if (employee is null)
            return new Response(InactiveUserAccountMessage);
        var employeeLoginDto = user.MapToEmployeeLoginDto();
        employee.MapToEmployeeLoginDto(employeeLoginDto);
        employeeLoginDto.AccessToken = _tokenService.CreateAccessToken(employeeLoginDto.MapToEmployeeClaims());
        employeeLoginDto.RefreshToken = user.RefreshToken;
        return new Response
        {
            Success = true,
            Data = employeeLoginDto,
            Message = SuccessfulLoginMessage
        };
    }
}
