namespace DentallApp.Features.Security.Users.UseCases;

public class UserLoginRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
}

public class UserLoginResponse
{
    public int UserId { get; set; }
    public int PersonId { get; set; }
    public string UserName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string GenderName { get; set; }
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string FullName { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
    public int? GenderId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class EmployeeLoginResponse : UserLoginResponse
{
    public int EmployeeId { get; set; }
    public int OfficeId { get; set; }
    public string OfficeName { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
}

public class UserLoginUseCase
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public UserLoginUseCase(
        AppDbContext context, 
        IUserRepository userRepository, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Execute(UserLoginRequest request)
    {
        var user = await _userRepository.GetFullUserProfileAsync(request.UserName);
        if (user is null || !_passwordHasher.Verify(request.UserName, user.Password))
            return new Response(EmailOrPasswordIncorrectMessage);

        if (user.IsUnverified())
            return new Response(EmailNotConfirmedMessage);

        user.RefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = _tokenService.CreateExpiryForRefreshToken();
        await _context.SaveChangesAsync();

        if (user.IsBasicUser())
        {
            var userLoginResponse = new UserLoginResponse();
            UserLoginMapper.MapToUserLoginResponse(source: user, destination: userLoginResponse);
            userLoginResponse.AccessToken = _tokenService.CreateAccessToken(userLoginResponse.MapToUserClaims());
            userLoginResponse.RefreshToken = user.RefreshToken;
            return new Response
            {
                Success = true,
                Data    = userLoginResponse,
                Message = SuccessfulLoginMessage
            };
        }

        var employee = await _context.Set<Employee>()
            .Include(employee => employee.Office)
            .Where(employee => employee.UserId == user.Id)
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (employee.IsInactive())
            return new Response(InactiveUserAccountMessage);

        var employeeLoginResponse = MapToEmployeeLoginResponse(employee);
        UserLoginMapper.MapToUserLoginResponse(source: user, destination: employeeLoginResponse);
        employeeLoginResponse.AccessToken = _tokenService.CreateAccessToken(MapToEmployeeClaims(employeeLoginResponse));
        employeeLoginResponse.RefreshToken = user.RefreshToken;
        return new Response
        {
            Success = true,
            Data    = employeeLoginResponse,
            Message = SuccessfulLoginMessage
        };
    }

    private static EmployeeClaims MapToEmployeeClaims(EmployeeLoginResponse source)
        => new()
        {
            UserId     = source.UserId,
            PersonId   = source.PersonId,
            EmployeeId = source.EmployeeId,
            OfficeId   = source.OfficeId,
            UserName   = source.UserName,
            FullName   = source.FullName,
            Roles      = source.Roles
        };

    private static EmployeeLoginResponse MapToEmployeeLoginResponse(Employee source)
        => new()
        {
            EmployeeId          = source.Id,
            OfficeId            = source.OfficeId,
            OfficeName          = source.Office.Name,
            PostgradeUniversity = source.PostgradeUniversity,
            PregradeUniversity  = source.PregradeUniversity
        };
}

public static class UserLoginMapper
{
    public static void MapToUserLoginResponse(User source, UserLoginResponse destination)
    {
        destination.Document   = source.Person.Document;
        destination.Names      = source.Person.Names;
        destination.LastNames  = source.Person.LastNames;
        destination.FullName   = source.Person.FullName;
        destination.CellPhone  = source.Person.CellPhone;
        destination.DateBirth  = source.Person.DateBirth;
        destination.GenderName = source.Person.Gender?.Name;
        destination.GenderId   = source.Person.GenderId;
        destination.UserId     = source.Id;
        destination.PersonId   = source.PersonId;
        destination.UserName   = source.UserName;
        destination.Roles      = source.UserRoles
            .OrderBy(role => role.RoleId)
            .Select(role => role.Role.Name);
    }

    public static UserClaims MapToUserClaims(this UserLoginResponse source)
        => new()
        {
            UserId   = source.UserId,
            PersonId = source.PersonId,
            UserName = source.UserName,
            FullName = source.FullName,
            Roles    = source.Roles
        };
}
