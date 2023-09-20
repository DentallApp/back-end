namespace DentallApp.Infrastructure.Persistence.EntityConfigurations;

public class EmployeeScheduleConfiguration : IEntityTypeConfiguration<EmployeeSchedule>
{
    public void Configure(EntityTypeBuilder<EmployeeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
