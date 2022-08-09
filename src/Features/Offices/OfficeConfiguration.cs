namespace DentallApp.Features.Offices;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
