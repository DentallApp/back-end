namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class AppointmentStatusSeedData
{
    public static ModelBuilder CreateDefaultAppointmentStatus(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new AppointmentStatus
            {
                Id = 1,
                Name = StatusType.Scheduled
            },
            new AppointmentStatus
            {
                Id = 2,
                Name = StatusType.NotAssisted
            },
            new AppointmentStatus
            {
                Id = 3,
                Name = StatusType.Assisted
            },
            new AppointmentStatus
            {
                Id = 4,
                Name = StatusType.Canceled
            }
        );
        return builder;
    }
}
