namespace DentallApp.Features.PersonalInformation.Genders;

public static class GenderSeedData
{
    public static ModelBuilder CreateGenders(this ModelBuilder builder)
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