namespace DentallApp.Core.Security.Users.UseCases;

public class CreateBasicUserRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }

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

public class CreateBasicUserValidator : AbstractValidator<CreateBasicUserRequest>
{
    public CreateBasicUserValidator()
    {
        RuleFor(request => request.UserName)
            .NotEmpty()
            .EmailAddress();
        RuleFor(request => request.Password).MustBeSecurePassword();
        RuleFor(request => request.Document).NotEmpty();
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
    }
}

public class CreateBasicUserUseCase(
    DbContext context,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    IEmailService emailService,
    CreateBasicUserValidator validator)
{
    public async Task<Result> ExecuteAsync(CreateBasicUserRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        if (await userRepository.UserExistsAsync(request.UserName))
            return Result.Conflict(Messages.UsernameAlreadyExists);

        var passwordHash = passwordHasher.HashPassword(request.Password);
        var user = request.MapToUser(passwordHash);
        context.Add(new UserRole { User = user, RoleId = (int)Role.Predefined.Unverified });
        await context.SaveChangesAsync();

        var userClaims = new UserClaims
        {
            UserId   = user.Id,
            PersonId = user.PersonId,
            UserName = request.UserName,
            FullName = request.Names + " " + request.LastNames,
            Roles    = new List<string> { RoleName.Unverified }
        };
        var emailVerificationToken = tokenService.CreateEmailVerificationToken(userClaims);
        await emailService.SendEmailForVerificationAsync(request.UserName, request.Names, emailVerificationToken);
        return Result.CreatedResource(Messages.CreateBasicUserAccount);
    }
}
