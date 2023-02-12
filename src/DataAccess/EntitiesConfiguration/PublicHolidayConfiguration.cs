namespace DentallApp.DataAccess.EntitiesConfiguration;

public class PublicHolidayConfiguration : IEntityTypeConfiguration<PublicHoliday>
{
    public void Configure(EntityTypeBuilder<PublicHoliday> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
