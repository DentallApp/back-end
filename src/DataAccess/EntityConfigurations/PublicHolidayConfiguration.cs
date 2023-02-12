namespace DentallApp.DataAccess.EntityConfigurations;

public class PublicHolidayConfiguration : IEntityTypeConfiguration<PublicHoliday>
{
    public void Configure(EntityTypeBuilder<PublicHoliday> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
