namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class KinshipSeedData
{
    public static ModelBuilder CreateDefaultKinships(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new Kinship
            {
                Id = 1,
                Name = KinshipName.Spouse
            },
            new Kinship
            {
                Id = 2,
                Name = KinshipName.Child
            },
            new Kinship
            {
                Id = 3,
                Name = KinshipName.Other
            }
        );
        return builder;
    }
}
