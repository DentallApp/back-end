namespace DentallApp.Core.Security.Employees.UseCases;

public class DeleteEmployeeUseCase(DbContext context, ICurrentEmployee currentEmployee)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        if (id == currentEmployee.EmployeeId)
            return Result.Forbidden(Messages.CannotRemoveYourOwnProfile);

        var employee = await context.Set<Employee>()
            .Include(employee => employee.User.UserRoles)
            .Where(employee => employee.Id == id)
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
