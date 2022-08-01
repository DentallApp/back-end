namespace DentallApp.Features.Kinships;

public class KinshipConfiguration : IEntityTypeConfiguration<Kinship>
{
    public void Configure(EntityTypeBuilder<Kinship> builder)
    {
        builder.HasData(
            new Kinship
            {
                Id = KinshipsId.Spouse,
                Name = KinshipsName.Spouse,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Kinship
            {
                Id = KinshipsId.Child,
                Name = KinshipsName.Child,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Kinship
            {
                Id = KinshipsId.Other,
                Name = KinshipsName.Other,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
