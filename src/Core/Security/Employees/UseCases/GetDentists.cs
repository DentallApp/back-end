namespace DentallApp.Core.Security.Employees.UseCases;

public class GetDentistsRequest
{
    public int OfficeId { get; init; }
    public bool? IsDentistDeleted { get; init; }
}

public class GetDentistsResponse
{
    public int EmployeeId { get; init; }
    public string FullName { get; init; }
}

public class GetDentistsUseCase(DbContext context, ICurrentEmployee currentEmployee)
{
    public async Task<ListedResult<GetDentistsResponse>> ExecuteAsync(GetDentistsRequest request)
    {
        if (!currentEmployee.IsSuperAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden();

        var queryable = 
           (from employee in context.Set<Employee>()
            join person in context.Set<Person>() on employee.PersonId equals person.Id
            join userRole in context.Set<UserRole>() on employee.UserId equals userRole.UserId
            where userRole.RoleId == (int)Role.Predefined.Dentist
            select new { employee, person });

        var dentists = await queryable
            .OptionalWhere(request.OfficeId, o => o.employee.OfficeId == request.OfficeId)
            .OptionalWhere(request.IsDentistDeleted, o => o.employee.IsDeleted == request.IsDentistDeleted)
            .Select(o => new GetDentistsResponse
            {
                EmployeeId = o.employee.Id,
                FullName   = o.person.FullName
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return Result.ObtainedResources(dentists);
    }
}
