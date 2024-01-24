namespace DentallApp.Features.Security.Employees.UseCases;

public class DeleteEmployeeUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int employeeId, ClaimsPrincipal currentEmployee)
    {
        var employee = await context.Set<Employee>()
            .Include(employee => employee.User.UserRoles)
            .Where(employee => employee.Id == employeeId)
            .FirstOrDefaultAsync();

        if (employee is null)
            return Result.NotFound(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return Result.Forbidden(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return Result.Forbidden(CannotRemoveSuperadminMessage);

        context.SoftDelete(employee);
        await context.SaveChangesAsync();
        return Result.DeletedResource();
    }
}
