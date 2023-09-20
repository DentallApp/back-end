namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class OfficeScheduleSeedData
{
    public static ModelBuilder CreateDefaultOfficeSchedules(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new OfficeSchedule
            {
                Id = 1,
                WeekDayId = 1,
                OfficeId = 1,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 2,
                WeekDayId = 2,
                OfficeId = 1,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 3,
                WeekDayId = 3,
                OfficeId = 1,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 4,
                WeekDayId = 4,
                OfficeId = 1,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 5,
                WeekDayId = 5,
                OfficeId = 1,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            }
        );

        builder.AddSeedData(
            new OfficeSchedule
            {
                Id = 6,
                WeekDayId = 1,
                OfficeId = 2,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 7,
                WeekDayId = 2,
                OfficeId = 2,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 8,
                WeekDayId = 3,
                OfficeId = 2,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 9,
                WeekDayId = 4,
                OfficeId = 2,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 10,
                WeekDayId = 5,
                OfficeId = 2,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            }
        );

        builder.AddSeedData(
            new OfficeSchedule
            {
                Id = 11,
                WeekDayId = 1,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 12,
                WeekDayId = 2,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 13,
                WeekDayId = 3,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 14,
                WeekDayId = 4,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 15,
                WeekDayId = 5,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            },
            new OfficeSchedule
            {
                Id = 16,
                WeekDayId = 6,
                OfficeId = 3,
                StartHour = new TimeSpan(07, 0, 0),
                EndHour = new TimeSpan(18, 0, 0)
            }
        );
        return builder;
    }
}
