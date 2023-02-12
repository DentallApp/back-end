namespace DentallApp.DataAccess.EntitiesConfiguration;

public class GeneralTreatmentConfiguration : IEntityTypeConfiguration<GeneralTreatment>
{
    public void Configure(EntityTypeBuilder<GeneralTreatment> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
