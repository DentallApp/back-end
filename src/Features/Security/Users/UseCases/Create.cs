namespace DentallApp.Features.Security.Users.UseCases;

public class CreateBasicUserRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }

    public User MapToUser(string password)
    {
        var person = new Person
        {
            Document  = Document,
            Names     = Names,
            LastNames = LastNames,
            CellPhone = CellPhone,
            Email     = UserName,
            DateBirth = DateBirth,
            GenderId  = GenderId
        };
        var user = new User
        {
            UserName = UserName,
            Password = password,
            Person   = person
        };
        return user;
    }
}

public class CreateBasicUserUseCase
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public CreateBasicUserUseCase(
        AppDbContext context,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher, 
        ITokenService tokenService,
        IEmailService emailService)
    {
        _context = context;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<Response> Execute(CreateBasicUserRequest request)
    {
        if (await _userRepository.UserExists(request.UserName))
            return new Response(UsernameAlreadyExistsMessage);

        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var user = request.MapToUser(passwordHash);
        _context.Add(new UserRole { User = user, RoleId = RolesId.Unverified });
        await _context.SaveChangesAsync();

        var userClaims = new UserClaims
        {
            UserId   = user.Id,
            PersonId = user.PersonId,
            UserName = request.UserName,
            FullName = request.Names + " " + request.LastNames,
            Roles    = new List<string> { RolesName.Unverified }
        };
        var emailVerificationToken = _tokenService.CreateEmailVerificationToken(userClaims);
        await _emailService.SendEmailForVerificationAsync(request.UserName, request.Names, emailVerificationToken);

        return new Response
        {
            Success = true,
            Message = CreateBasicUserAccountMessage
        };
    }
}
