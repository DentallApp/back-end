namespace DentallApp.Features.Genders;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasData(
            new Gender
            {
                Id = GendersId.Male,
                Name = GendersName.Male,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Gender
            {
                Id = GendersId.Female,
                Name = GendersName.Female,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}