namespace DentallApp.DataAccess.EntitiesConfiguration;

public class OfficeScheduleConfiguration : IEntityTypeConfiguration<OfficeSchedule>
{
    public void Configure(EntityTypeBuilder<OfficeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
