namespace DentallApp.DataAccess.EntitiesConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
