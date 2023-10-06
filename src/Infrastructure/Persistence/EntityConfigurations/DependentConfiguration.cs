namespace DentallApp.Infrastructure.Persistence.EntityConfigurations;

public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
{
    public void Configure(EntityTypeBuilder<Dependent> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
