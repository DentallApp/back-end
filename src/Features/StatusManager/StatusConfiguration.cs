namespace DentallApp.Features.StatusManager;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData(
            new Status
            {
                Id = StatusId.Active,
                Name = "ACTIVO",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Status
            {
                Id = StatusId.Inactive,
                Name = "INACTIVO",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
