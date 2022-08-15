namespace DentallApp.Features.Kinships;

public class KinshipConfiguration : IEntityTypeConfiguration<Kinship>
{
    public void Configure(EntityTypeBuilder<Kinship> builder)
    {
        builder.AddSeedData(
            new Kinship
            {
                Id = KinshipsId.Spouse,
                Name = KinshipsName.Spouse
            },
            new Kinship
            {
                Id = KinshipsId.Child,
                Name = KinshipsName.Child
            },
            new Kinship
            {
                Id = KinshipsId.Other,
                Name = KinshipsName.Other
            }
        );
    }
}
