namespace DentallApp.Core.Security.Employees.UseCases;

public class UpdateCurrentEmployeeRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }

    public void MapToEmployee(Employee employee)
    {
        employee.Person.Names        = Names;
        employee.Person.LastNames    = LastNames;
        employee.Person.CellPhone    = CellPhone;
        employee.Person.DateBirth    = DateBirth;
        employee.Person.GenderId     = GenderId;
        employee.PregradeUniversity  = PregradeUniversity;
        employee.PostgradeUniversity = PostgradeUniversity;
    }
}

public class UpdateCurrentEmployeeValidator : AbstractValidator<UpdateCurrentEmployeeRequest>
{
    public UpdateCurrentEmployeeValidator()
    {
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
    }
}

/// <summary>
/// Current Employee is the Employee who is current logged in. 
/// The current employee can edit his own information.
/// </summary>
public class UpdateCurrentEmployeeUseCase(
    DbContext context, 
    ICurrentEmployee currentEmployee,
    UpdateCurrentEmployeeValidator validator)
{
    public async Task<Result> ExecuteAsync(UpdateCurrentEmployeeRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var employee = await context.Set<Employee>()
            .Include(employee => employee.Person)
            .Where(employee => employee.Id == currentEmployee.EmployeeId)
            .FirstOrDefaultAsync();

        if (employee is null)
            return Result.NotFound(Messages.EmployeeNotFound);

        if (employee.Id != currentEmployee.EmployeeId)
            return Result.Forbidden(Messages.CannotUpdateAnotherUserResource);

        request.MapToEmployee(employee);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
