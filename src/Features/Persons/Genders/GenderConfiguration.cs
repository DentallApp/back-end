namespace DentallApp.Features.Persons.Genders;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
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
    }
}