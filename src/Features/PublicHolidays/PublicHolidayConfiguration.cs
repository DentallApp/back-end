namespace DentallApp.Features.PublicHolidays;

public class PublicHolidayConfiguration : IEntityTypeConfiguration<PublicHoliday>
{
    public void Configure(EntityTypeBuilder<PublicHoliday> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
