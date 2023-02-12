namespace DentallApp.DataAccess.SeedsData;

public static class GenderSeedData
{
    public static ModelBuilder CreateDefaultGenders(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new Gender
            {
                Id = GendersId.Male,
                Name = GendersName.Male
            },
            new Gender
            {
                Id = GendersId.Female,
                Name = GendersName.Female
            }
        );
        return builder;
    }
}