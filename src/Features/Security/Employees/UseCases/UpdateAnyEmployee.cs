namespace DentallApp.Features.Security.Employees.UseCases;

public class UpdateAnyEmployeeRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime DateBirth { get; init; }
    public int GenderId { get; init; }
    public int OfficeId { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }

    [Required]
    [MaxLength(NumberRoles.MaxRole)]
    [MinLength(NumberRoles.MinRole)]
    public List<int> Roles { get; init; }
    public List<int> SpecialtiesId { get; init; }
    public bool IsDeleted { get; init; }

    public void MapToEmployee(Employee employee)
    {
        employee.User.UserName       = Email;
        employee.Person.Email        = Email;
        employee.Person.Document     = Document;
        employee.Person.Names        = Names;
        employee.Person.LastNames    = LastNames;
        employee.Person.CellPhone    = CellPhone;
        employee.Person.DateBirth    = DateBirth;
        employee.Person.GenderId     = GenderId;
        employee.OfficeId            = OfficeId;
        employee.PregradeUniversity  = PregradeUniversity;
        employee.PostgradeUniversity = PostgradeUniversity;
        employee.IsDeleted           = IsDeleted;
    }
}

public class UpdateAnyEmployeeUseCase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IEmployeeSpecialtyRepository _employeeSpecialtyRepository;

    public UpdateAnyEmployeeUseCase(
        AppDbContext context, 
        IPasswordHasher passwordHasher, 
        IUserRoleRepository userRoleRepository, 
        IEmployeeSpecialtyRepository employeeSpecialtyRepository)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _userRoleRepository = userRoleRepository;
        _employeeSpecialtyRepository = employeeSpecialtyRepository;
    }

    public async Task<Response> Execute(int employeeId, ClaimsPrincipal currentEmployee, UpdateAnyEmployeeRequest request)
    {
        var employeeToEdit = await _context.Set<Employee>()
            .Include(employee => employee.Person)
            .Include(employee => employee.User.UserRoles)
            .Include(employee => employee.EmployeeSpecialties)
            .Where(employee => employee.Id == employeeId)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (employeeToEdit is null)
            return new Response(EmployeeNotFoundMessage);

        // An admin cannot edit a Superadmin's profile.
        // However, the Superadmin can edit his own profile.
        if (!currentEmployee.IsSuperAdmin() && employeeToEdit.IsSuperAdmin())
            return new Response(CannotEditSuperadminMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employeeToEdit.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (currentEmployee.HasNotPermissions(request.Roles, employeeToEdit.Id))
            return new Response(PermitsNotGrantedMessage);

        if (request.Password is not null)
            employeeToEdit.User.Password = _passwordHasher.HashPassword(request.Password);

        var specialtiesId = request.SpecialtiesId ?? Enumerable.Empty<int>().ToList();
        _employeeSpecialtyRepository
            .UpdateEmployeeSpecialties(employeeToEdit.Id, employeeToEdit.EmployeeSpecialties, specialtiesId);

        _userRoleRepository
            .UpdateUserRoles(employeeToEdit.UserId, employeeToEdit.User.UserRoles, rolesId: request.Roles);

        request.MapToEmployee(employeeToEdit);
        if (employeeToEdit.IsInactive())
        {
            employeeToEdit.User.RefreshToken = null;
            employeeToEdit.User.RefreshTokenExpiry = null;
        }
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
