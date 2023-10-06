namespace DentallApp.Infrastructure.Persistence.EntityConfigurations;

public class PublicHolidayConfiguration : IEntityTypeConfiguration<PublicHoliday>
{
    public void Configure(EntityTypeBuilder<PublicHoliday> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
