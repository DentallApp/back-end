namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class AppointmentStatusSeedData
{
    public static ModelBuilder CreateDefaultAppointmentStatus(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Scheduled,
                Name = StatusType.Scheduled
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.NotAssisted,
                Name = StatusType.NotAssisted
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Assisted,
                Name = StatusType.Assisted
            },
            new AppointmentStatus
            {
                Id = AppointmentStatusId.Canceled,
                Name = StatusType.Canceled
            }
        );
        return builder;
    }
}
