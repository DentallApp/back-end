namespace DentallApp.Features.Security.Employees.UseCases;

public class DeleteEmployeeUseCase
{
    private readonly AppDbContext _context;

    public DeleteEmployeeUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int employeeId, ClaimsPrincipal currentEmployee)
    {
        var employee = await _context.Set<Employee>()
            .Include(employee => employee.User.UserRoles)
            .Where(employee => employee.Id == employeeId)
            .FirstOrDefaultAsync();

        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotRemoveSuperadminMessage);

        _context.SoftDelete(employee);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
