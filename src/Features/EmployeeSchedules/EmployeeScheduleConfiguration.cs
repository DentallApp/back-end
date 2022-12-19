namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleConfiguration : IEntityTypeConfiguration<EmployeeSchedule>
{
    public void Configure(EntityTypeBuilder<EmployeeSchedule> builder)
    {
        builder.HasQueryFilterSoftDelete();

        if (!WebHostEnvironment.IsDevelopment())
            return;

        builder.AddSeedData(
            new EmployeeSchedule
            {
                Id = 1,
                EmployeeId = 2,
                WeekDayId = 1,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 2,
                EmployeeId = 2,
                WeekDayId = 2,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 3,
                EmployeeId = 2,
                WeekDayId = 3,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 4,
                EmployeeId = 2,
                WeekDayId = 4,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 5,
                EmployeeId = 2,
                WeekDayId = 5,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            }
        );

        builder.AddSeedData(
            new EmployeeSchedule
            {
                Id = 6,
                EmployeeId = 5,
                WeekDayId = 1,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 7,
                EmployeeId = 5,
                WeekDayId = 2,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 8,
                EmployeeId = 5,
                WeekDayId = 3,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 9,
                EmployeeId = 5,
                WeekDayId = 4,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 10,
                EmployeeId = 5,
                WeekDayId = 5,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            }
        );

        builder.AddSeedData(
            new EmployeeSchedule
            {
                Id = 11,
                EmployeeId = 6,
                WeekDayId = 1,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 12,
                EmployeeId = 6,
                WeekDayId = 2,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 13,
                EmployeeId = 6,
                WeekDayId = 3,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 14,
                EmployeeId = 6,
                WeekDayId = 4,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            },
            new EmployeeSchedule
            {
                Id = 15,
                EmployeeId = 6,
                WeekDayId = 5,
                MorningStartHour = new TimeSpan(07, 0, 0),
                MorningEndHour = new TimeSpan(12, 0, 0),
                AfternoonStartHour = new TimeSpan(13, 0, 0),
                AfternoonEndHour = new TimeSpan(18, 0, 0)
            }
        );
    }
}
