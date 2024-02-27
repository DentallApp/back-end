namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class WeekDaySeedData
{
    public static ModelBuilder CreateDefaultWeekDays(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new WeekDay
            {
                Id = 1,
                Name = WeekDayName.Monday
            },
            new WeekDay
            {
                Id = 2,
                Name = WeekDayName.Tuesday
            },
            new WeekDay
            {
                Id = 3,
                Name = WeekDayName.Wednesday
            },
            new WeekDay
            {
                Id = 4,
                Name = WeekDayName.Thursday
            },
            new WeekDay
            {
                Id = 5,
                Name = WeekDayName.Friday
            },
            new WeekDay
            {
                Id = 6,
                Name = WeekDayName.Saturday
            },
            new WeekDay
            {
                Id = 7,
                Name = WeekDayName.Sunday
            }
        );
        return builder;
    }
}
