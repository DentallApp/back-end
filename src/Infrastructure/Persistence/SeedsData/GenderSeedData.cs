namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class GenderSeedData
{
    public static ModelBuilder CreateDefaultGenders(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new Gender
            {
                Id = 1,
                Name = GenderName.Male
            },
            new Gender
            {
                Id = 2,
                Name = GenderName.Female
            }
        );
        return builder;
    }
}