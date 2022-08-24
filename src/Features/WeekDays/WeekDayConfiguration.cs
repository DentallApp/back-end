namespace DentallApp.Features.WeekDays;

public class WeekDayConfiguration : IEntityTypeConfiguration<WeekDay>
{
    public void Configure(EntityTypeBuilder<WeekDay> builder)
    {
        builder.AddSeedData(
            new WeekDay
            {
                Id = 1,
                Name = "Lunes"
            },
            new WeekDay
            {
                Id = 2,
                Name = "Martes"
            },
            new WeekDay
            {
                Id = 3,
                Name = "Miércoles"
            },
            new WeekDay
            {
                Id = 4,
                Name = "Jueves"
            },
            new WeekDay
            {
                Id = 5,
                Name = "Viernes"
            },
            new WeekDay
            {
                Id = 6,
                Name = "Sábado"
            }
        );
    }
}
