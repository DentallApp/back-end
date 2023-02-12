namespace DentallApp.DataAccess.EntitiesConfiguration;

public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
{
    public void Configure(EntityTypeBuilder<Dependent> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
