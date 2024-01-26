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
            return Result.NotFound(Messages.EmployeeNotFound);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        if (employee.IsSuperAdmin())
            return Result.Forbidden(Messages.CannotRemoveSuperadmin);

        context.SoftDelete(employee);
        await context.SaveChangesAsync();
        return Result.DeletedResource();
    }
}
