﻿namespace DentallApp.Core.Security.Employees.UseCases;

public class UpdateAnyEmployeeRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
    public int OfficeId { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }
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

public class UpdateAnyEmployeeValidator : AbstractValidator<UpdateAnyEmployeeRequest>
{
    public UpdateAnyEmployeeValidator(IIdentityDocumentValidator documentValidator)
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(request => request.Password).MustBeSecurePassword();
        RuleFor(request => request.Document)
            .MustBeValidIdentityDocument(documentValidator);
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
        RuleFor(request => request.OfficeId).GreaterThan(0);
        RuleFor(request => request.Roles).NotEmpty();
        RuleForEach(request => request.Roles)
            .InclusiveBetween(Role.Range.Min, Role.Range.Max);
        RuleForEach(request => request.SpecialtiesId).GreaterThan(0);
    }
}

public class UpdateAnyEmployeeUseCase(
    DbContext context,
    IPasswordHasher passwordHasher,
    IEntityService<UserRole> userRoleService,
    IEntityService<EmployeeSpecialty> employeeSpecialtyService,
    ICurrentEmployee currentEmployee,
    UpdateAnyEmployeeValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdateAnyEmployeeRequest request)
    {
        if (currentEmployee.IsAdmin() && id == currentEmployee.EmployeeId)
            return Result.Forbidden(Messages.CannotEditYourOwnProfile);

        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var employeeToEdit = await context.Set<Employee>()
            .Include(employee => employee.Person)
            .Include(employee => employee.User.UserRoles)
            .Include(employee => employee.EmployeeSpecialties)
            .Where(employee => employee.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (employeeToEdit is null)
            return Result.NotFound(Messages.EmployeeNotFound);

        // An admin cannot edit a Superadmin's profile.
        // However, the Superadmin can edit his own profile.
        if (!currentEmployee.IsSuperAdmin() && employeeToEdit.IsSuperAdmin())
            return  Result.Forbidden(Messages.CannotEditSuperadmin);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employeeToEdit.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        if (currentEmployee.HasNotPermissions(request.Roles))
            return Result.Forbidden(Messages.PermitsNotGranted);

        if (request.Password is not null)
            employeeToEdit.User.Password = passwordHasher.HashPassword(request.Password);

        var specialtiesId = request.SpecialtiesId ?? Enumerable.Empty<int>().ToList();
        UpdateEmployeeSpecialties(employeeToEdit.Id, employeeToEdit.EmployeeSpecialties, specialtiesId);
        UpdateUserRoles(employeeToEdit.UserId, employeeToEdit.User.UserRoles, rolesId: request.Roles);

        request.MapToEmployee(employeeToEdit);
        if (employeeToEdit.IsInactive())
        {
            employeeToEdit.User.RefreshToken = null;
            employeeToEdit.User.RefreshTokenExpiry = null;
        }
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }

    /// <summary>
    /// Updates the roles to a user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="currentUserRoles">A collection with the current roles of a user.</param>
    /// <param name="rolesId">A collection of role identifiers obtained from a client.</param>
    private void UpdateUserRoles(
        int userId, 
        List<UserRole> currentUserRoles, 
        List<int> rolesId)
    {
        userRoleService.Update(userId, ref currentUserRoles, ref rolesId);
    }

    /// <summary>
    /// Updates the specialties to a employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to update.</param>
    /// <param name="currentEmployeeSpecialties">A collection with the current specialties of a employee.</param>
    /// <param name="specialtiesId">A collection of specialty identifiers obtained from a client.</param>
    private void UpdateEmployeeSpecialties(
        int employeeId, 
        List<EmployeeSpecialty> currentEmployeeSpecialties, 
        List<int> specialtiesId)
    {
        employeeSpecialtyService.Update(employeeId, ref currentEmployeeSpecialties, ref specialtiesId);
    }
}
