namespace DentallApp.Features.Security.Employees.UseCases;

public class CreateEmployeeRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public int OfficeId { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }

    [Required]
    [MaxLength(NumberRoles.MaxRole)]
    [MinLength(NumberRoles.MinRole)]
    public IEnumerable<int> Roles { get; init; }
    public IEnumerable<int> SpecialtiesId { get; init; }
}

public static class CreateEmployeeMapper
{
    public static Employee MapToEmployee(this CreateEmployeeRequest request, string password)
    {
        var person = new Person
        {
            Document  = request.Document,
            Names     = request.Names,
            LastNames = request.LastNames,
            CellPhone = request.CellPhone,
            Email     = request.UserName,
            DateBirth = request.DateBirth,
            GenderId  = request.GenderId
        };
        var user = new User
        {
            UserName = request.UserName,
            Password = password,
            Person   = person
        };
        var employee = new Employee
        {
            OfficeId            = request.OfficeId,
            PostgradeUniversity = request.PostgradeUniversity,
            PregradeUniversity  = request.PregradeUniversity,
            User                = user,
            Person              = person
        };
        return employee;
    }
}

public class CreateEmployeeUseCase
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateEmployeeUseCase(
        AppDbContext context, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response<InsertedIdDto>> Execute(ClaimsPrincipal currentEmployee, CreateEmployeeRequest request)
    {
        if (await _userRepository.UserExists(request.UserName))
            return new Response<InsertedIdDto>(UsernameAlreadyExistsMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return new Response<InsertedIdDto>(OfficeNotAssignedMessage);

        if (currentEmployee.HasNotPermissions(request.Roles))
            return new Response<InsertedIdDto>(PermitsNotGrantedMessage);

        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var employee = request.MapToEmployee(passwordHash);
        var rolesId = request.Roles.RemoveDuplicates();
        foreach (var roleId in rolesId)
        {
            _context.Add(new UserRole { User = employee.User, RoleId = roleId });
        }

        var specialtiesId = (request.SpecialtiesId ?? Enumerable.Empty<int>()).RemoveDuplicates();
        foreach (var specialtyId in specialtiesId)
        {
            _context.Add(new EmployeeSpecialty { Employee = employee, SpecialtyId = specialtyId });
        }

        _context.Add(employee);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = employee.Id },
            Success = true,
            Message = CreateEmployeeAccountMessage
        };
    }
}
