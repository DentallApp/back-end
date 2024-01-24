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

    public Employee MapToEmployee(string password)
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
        var employee = new Employee
        {
            OfficeId            = OfficeId,
            PostgradeUniversity = PostgradeUniversity,
            PregradeUniversity  = PregradeUniversity,
            User                = user,
            Person              = person
        };
        return employee;
    }
}

public class CreateEmployeeUseCase(
    DbContext context,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
{
    public async Task<Result<CreatedId>> ExecuteAsync(ClaimsPrincipal currentEmployee, CreateEmployeeRequest request)
    {
        if (await userRepository.UserExistsAsync(request.UserName))
            return Result.Conflict(UsernameAlreadyExistsMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden(OfficeNotAssignedMessage);

        if (currentEmployee.HasNotPermissions(request.Roles))
            return Result.Forbidden(PermitsNotGrantedMessage);

        var passwordHash = passwordHasher.HashPassword(request.Password);
        var employee = request.MapToEmployee(passwordHash);
        var rolesId = request.Roles.RemoveDuplicates();
        foreach (var roleId in rolesId)
        {
            context.Add(new UserRole { User = employee.User, RoleId = roleId });
        }

        var specialtiesId = (request.SpecialtiesId ?? Enumerable.Empty<int>()).RemoveDuplicates();
        foreach (var specialtyId in specialtiesId)
        {
            context.Add(new EmployeeSpecialty { Employee = employee, SpecialtyId = specialtyId });
        }

        context.Add(employee);
        await context.SaveChangesAsync();
        return Result.CreatedResource(employee.Id, CreateEmployeeAccountMessage);
    }
}
