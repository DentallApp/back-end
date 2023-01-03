namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleConfiguration : IEntityTypeConfiguration<EmployeeSchedule>
{
    public void Configure(EntityTypeBuilder<EmployeeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
