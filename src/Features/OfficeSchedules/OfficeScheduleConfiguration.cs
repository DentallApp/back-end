namespace DentallApp.Features.OfficeSchedules;

public class OfficeScheduleConfiguration : IEntityTypeConfiguration<OfficeSchedule>
{
    public void Configure(EntityTypeBuilder<OfficeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
