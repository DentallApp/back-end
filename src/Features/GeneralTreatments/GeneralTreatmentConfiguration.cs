namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentConfiguration : IEntityTypeConfiguration<GeneralTreatment>
{
    public void Configure(EntityTypeBuilder<GeneralTreatment> builder)
    {
        builder.HasQueryFilterSoftDelete();
    }
}
