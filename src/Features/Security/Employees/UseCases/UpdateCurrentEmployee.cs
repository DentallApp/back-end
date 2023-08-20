namespace DentallApp.Features.Security.Employees.UseCases;

public class UpdateCurrentEmployeeRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime DateBirth { get; init; }
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

/// <summary>
/// Current Employee is the Employee who is current logged in. 
/// The current employee can edit his own information.
/// </summary>
public class UpdateCurrentEmployeeUseCase
{
    private readonly AppDbContext _context;

    public UpdateCurrentEmployeeUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int currentEmployeeId, UpdateCurrentEmployeeRequest request)
    {
        var employee = await _context.Set<Employee>()
            .Include(employee => employee.Person)
            .Where(employee => employee.Id == currentEmployeeId)
            .FirstOrDefaultAsync();

        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (employee.Id != currentEmployeeId)
            return new Response(CannotUpdateAnotherUserResource);

        request.MapToEmployee(employee);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
