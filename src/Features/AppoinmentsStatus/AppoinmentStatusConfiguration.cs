namespace DentallApp.Features.AppoinmentsStatus;

public class AppoinmentStatusConfiguration : IEntityTypeConfiguration<AppoinmentStatus>
{
    public void Configure(EntityTypeBuilder<AppoinmentStatus> builder)
    {
        builder.AddSeedData(
            new AppoinmentStatus
            {
                Id = AppoinmentStatusId.Scheduled,
                Name = AppoinmentStatusName.Scheduled
            },
            new AppoinmentStatus
            {
                Id = 2,
                Name = "En proceso"
            },
            new AppoinmentStatus
            {
                Id = 3,
                Name = "No asistida"
            },
            new AppoinmentStatus
            {
                Id = 4,
                Name = "En consulta"
            },
            new AppoinmentStatus
            {
                Id = 5,
                Name = "Asistida"
            },
            new AppoinmentStatus
            {
                Id = 6,
                Name = "Cancelada"
            }
        );
    }
}
