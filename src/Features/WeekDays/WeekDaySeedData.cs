namespace DentallApp.Features.WeekDays;

public static class WeekDaySeedData
{
    public static ModelBuilder CreateDefaultWeekDays(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new WeekDay
            {
                Id = 1,
                Name = WeekDaysName.Monday
            },
            new WeekDay
            {
                Id = 2,
                Name = WeekDaysName.Tuesday
            },
            new WeekDay
            {
                Id = 3,
                Name = WeekDaysName.Wednesday
            },
            new WeekDay
            {
                Id = 4,
                Name = WeekDaysName.Thursday
            },
            new WeekDay
            {
                Id = 5,
                Name = WeekDaysName.Friday
            },
            new WeekDay
            {
                Id = 6,
                Name = WeekDaysName.Saturday
            }
        );
        return builder;
    }
}
