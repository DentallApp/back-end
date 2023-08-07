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
            var userLoginResponse = user.MapToUserLoginResponse();
            var userClaims = userLoginResponse.MapToUserClaims();
            userLoginResponse.AccessToken = _tokenService.CreateAccessToken(userClaims);
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

        employee.User = user;
        var employeeLoginResponse = employee.MapToEmployeeLoginResponse();
        var employeeClaims = employeeLoginResponse.MapToEmployeeClaims();
        employeeLoginResponse.AccessToken = _tokenService.CreateAccessToken(employeeClaims);
        employeeLoginResponse.RefreshToken = user.RefreshToken;
        return new Response
        {
            Success = true,
            Data    = employeeLoginResponse,
            Message = SuccessfulLoginMessage
        };
    }
}

public static class UserLoginMapper
{
    private static void MapToUserLoginResponse(User source, UserLoginResponse destination)
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

    public static UserLoginResponse MapToUserLoginResponse(this User user)
    {
        var response = new UserLoginResponse();
        MapToUserLoginResponse(user, response);
        return response;
    }

    public static EmployeeLoginResponse MapToEmployeeLoginResponse(this Employee employee)
    {
        var response = new EmployeeLoginResponse
        {
            EmployeeId          = employee.Id,
            OfficeId            = employee.OfficeId,
            OfficeName          = employee.Office.Name,
            PostgradeUniversity = employee.PostgradeUniversity,
            PregradeUniversity  = employee.PregradeUniversity
        };
        MapToUserLoginResponse(employee.User, response);
        return response;
    }

    public static UserClaims MapToUserClaims(this UserLoginResponse response)
    {
        return new()
        {
            UserId   = response.UserId,
            PersonId = response.PersonId,
            UserName = response.UserName,
            FullName = response.FullName,
            Roles    = response.Roles
        };
    }

    public static EmployeeClaims MapToEmployeeClaims(this EmployeeLoginResponse response)
    {
        return new()
        {
            UserId     = response.UserId,
            PersonId   = response.PersonId,
            EmployeeId = response.EmployeeId,
            OfficeId   = response.OfficeId,
            UserName   = response.UserName,
            FullName   = response.FullName,
            Roles      = response.Roles
        };
    }
}
