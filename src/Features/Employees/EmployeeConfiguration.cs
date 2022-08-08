namespace DentallApp.Features.Employees;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasQueryFilter(employee => employee.StatusId == StatusId.Active);
    }
}
