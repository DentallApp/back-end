namespace DentallApp.Features.UserRegistration;

public class UserRegisterService : IUserRegisterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public UserRegisterService(IUnitOfWork unitOfWork, 
                               IEmailService emailService, 
                               ITokenService tokenService,
                               IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    private User CreateUserAccount(UserInsertDto userInsertDto, params int[] rolesId)
    {
        var person = userInsertDto.MapToPerson();
        _unitOfWork.PersonRepository.Insert(person);
        var user = userInsertDto.MapToUser();
        _unitOfWork.UserRepository.Insert(user);
        user.Person = person;
        user.Password = _passwordHasher.HashPassword(userInsertDto.Password);
        foreach (var roleId in rolesId)
        {
            var userRole = new UserRole { RoleId = roleId };
            _unitOfWork.UserRoleRepository.Insert(userRole);
            userRole.User = user;
        }
        return user;
    }

    public async Task<Response> CreateBasicUserAccountAsync(UserInsertDto userInsertDto)
    {
        if (await _unitOfWork.UserRepository.UserExistsAsync(userInsertDto.UserName))
            return new Response(UsernameAlreadyExistsMessage);

        var user = CreateUserAccount(userInsertDto, RolesId.Unverified);
        await _unitOfWork.SaveChangesAsync();

        var emailVerificationToken = _tokenService.CreateEmailVerificationToken(userInsertDto.MapToUserClaims(user));
        await _emailService.SendEmailForVerificationAsync(userInsertDto.UserName, userInsertDto.Names, emailVerificationToken);

        return new Response
        {
            Success = true,
            Message = CreateBasicUserAccountMessage
        };
    }

    public async Task<Response> CreateEmployeeAccountAsync(ClaimsPrincipal claims, EmployeeInsertDto employeeInsertDto)
    {
        if (await _unitOfWork.UserRepository.UserExistsAsync(employeeInsertDto.UserName))
            return new Response(UsernameAlreadyExistsMessage);

        if (claims.IsInRole(RolesName.Admin) && claims.GetOfficeId() != employeeInsertDto.OfficeId)
            return new Response(OfficeNotAssigned);

        var user = CreateUserAccount(employeeInsertDto, employeeInsertDto.Roles);
        var employee = employeeInsertDto.MapToEmployee();
        _unitOfWork.EmployeeRepository.Insert(employee);
        employee.User = user;
        employee.Person = user.Person;
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = CreateEmployeeAccountMessage
        };
    }
}
