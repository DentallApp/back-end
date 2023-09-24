namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class AppointmentStatusSeedData
{
    public static ModelBuilder CreateDefaultAppointmentStatus(this ModelBuilder builder)
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
        return builder;
    }
}
