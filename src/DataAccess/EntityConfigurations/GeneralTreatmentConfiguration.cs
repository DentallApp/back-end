namespace DentallApp.DataAccess.EntityConfigurations;

public class GeneralTreatmentConfiguration : IEntityTypeConfiguration<GeneralTreatment>
{
    public void Configure(EntityTypeBuilder<GeneralTreatment> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
