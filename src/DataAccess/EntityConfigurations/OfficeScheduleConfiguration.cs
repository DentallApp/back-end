namespace DentallApp.DataAccess.EntityConfigurations;

public class OfficeScheduleConfiguration : IEntityTypeConfiguration<OfficeSchedule>
{
    public void Configure(EntityTypeBuilder<OfficeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
