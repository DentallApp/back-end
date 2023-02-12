namespace DentallApp.DataAccess.EntityConfigurations;

public class EmployeeScheduleConfiguration : IEntityTypeConfiguration<EmployeeSchedule>
{
    public void Configure(EntityTypeBuilder<EmployeeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
