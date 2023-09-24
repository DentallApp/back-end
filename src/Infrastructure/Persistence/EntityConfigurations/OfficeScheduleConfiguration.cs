namespace DentallApp.Infrastructure.Persistence.EntityConfigurations;

public class OfficeScheduleConfiguration : IEntityTypeConfiguration<OfficeSchedule>
{
    public void Configure(EntityTypeBuilder<OfficeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
