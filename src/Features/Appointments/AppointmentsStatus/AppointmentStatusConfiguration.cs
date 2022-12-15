namespace DentallApp.Features.Appointments.AppointmentsStatus;

public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
{
    public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
    {
        builder.AddSeedData(
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Scheduled,
                Name = AppointmentStatusName.Scheduled
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.NotAssisted,
                Name = AppointmentStatusName.NotAssisted
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Assisted,
                Name = AppointmentStatusName.Assisted
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Canceled,
                Name = AppointmentStatusName.Canceled
            }
        );
    }
}
