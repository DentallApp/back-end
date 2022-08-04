namespace DentallApp.Features.Dependents;

public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
{
    public void Configure(EntityTypeBuilder<Dependent> builder)
    {
        builder.HasQueryFilter(dependent => dependent.StatusId == StatusId.Active);
    }
}
