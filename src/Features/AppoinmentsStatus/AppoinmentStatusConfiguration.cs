namespace DentallApp.Features.AppoinmentsStatus;

public class AppoinmentStatusConfiguration : IEntityTypeConfiguration<AppoinmentStatus>
{
    public void Configure(EntityTypeBuilder<AppoinmentStatus> builder)
    {
        builder.HasData(
            new AppoinmentStatus
            {
                Id = AppoinmentStatusId.Scheduled,
                Name = AppoinmentStatusName.Scheduled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new AppoinmentStatus
            {
                Id = 2,
                Name = "En proceso",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new AppoinmentStatus
            {
                Id = 3,
                Name = "No asistida",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new AppoinmentStatus
            {
                Id = 4,
                Name = "En consulta",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new AppoinmentStatus
            {
                Id = 5,
                Name = "Asistida",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new AppoinmentStatus
            {
                Id = 6,
                Name = "Cancelada",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
