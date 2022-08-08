namespace DentallApp.Features.Offices;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasQueryFilter(office => office.StatusId == StatusId.Active);
    }
}
