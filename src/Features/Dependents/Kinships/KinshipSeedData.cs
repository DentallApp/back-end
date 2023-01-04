namespace DentallApp.Features.Dependents.Kinships;

public static class KinshipSeedData
{
    public static ModelBuilder CreateDefaultKinships(this ModelBuilder builder)
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
        return builder;
    }
}
