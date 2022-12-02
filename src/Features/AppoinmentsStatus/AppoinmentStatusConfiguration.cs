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
                Id = AppoinmentStatusId.NotAssisted,
                Name = AppoinmentStatusName.NotAssisted
            },
            new AppoinmentStatus
            {
                Id = AppoinmentStatusId.Assisted,
                Name = AppoinmentStatusName.Assisted
            },
            new AppoinmentStatus
            {
                Id = AppoinmentStatusId.Canceled,
                Name = AppoinmentStatusName.Canceled
            }
        );
    }
}
