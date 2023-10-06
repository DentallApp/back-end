namespace DentallApp.Infrastructure.Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
