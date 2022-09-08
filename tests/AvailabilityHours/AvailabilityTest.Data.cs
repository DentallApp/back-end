namespace DentallApp.Tests.AvailabilityHours;

public partial class AvailabilityTest
{
    public static IEnumerable<object[]> GetData()
    {
        var dentistStartHour = TimeSpan.Parse("7:00");
        var dentistEndHour   = TimeSpan.Parse("13:10");
        var unavailables = new List<UnavailableTimeRangeDto>
        {
            new() { StartHour = TimeSpan.Parse("9:00"),  EndHour = TimeSpan.Parse("9:30") },
            new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("10:30") },
            new() { StartHour = TimeSpan.Parse("11:00"), EndHour = TimeSpan.Parse("11:40") },
            new() { StartHour = TimeSpan.Parse("12:40"), EndHour = TimeSpan.Parse("13:10") }
        };

        yield return new object[]
        {
            "test1",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = dentistEndHour,
                ServiceDuration  = TimeSpan.FromMinutes(30),
                Unavailables     = unavailables
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:30") },
                new() { StartHour = TimeSpan.Parse("7:30"),  EndHour = TimeSpan.Parse("8:00") },
                new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("8:30") },
                new() { StartHour = TimeSpan.Parse("8:30"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("9:30"),  EndHour = TimeSpan.Parse("10:00") },
                new() { StartHour = TimeSpan.Parse("10:30"), EndHour = TimeSpan.Parse("11:00") },
                new() { StartHour = TimeSpan.Parse("11:40"), EndHour = TimeSpan.Parse("12:10") },
                new() { StartHour = TimeSpan.Parse("12:10"), EndHour = TimeSpan.Parse("12:40") }
            }
        };

        yield return new object[]
        {
            "test2",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = dentistEndHour,
                ServiceDuration  = TimeSpan.FromMinutes(40),
                Unavailables     = unavailables
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:40") },
                new() { StartHour = TimeSpan.Parse("7:40"),  EndHour = TimeSpan.Parse("8:20") },
                new() { StartHour = TimeSpan.Parse("8:20"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("11:40"), EndHour = TimeSpan.Parse("12:20") }
            }
        };

        yield return new object[]
        {
            "test3",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = dentistEndHour,
                ServiceDuration  = TimeSpan.FromMinutes(60),
                Unavailables     = unavailables
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"), EndHour = TimeSpan.Parse("8:00") },
                new() { StartHour = TimeSpan.Parse("8:00"), EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("11:40"), EndHour = TimeSpan.Parse("12:40") }
            }
        };

        yield return new object[]
        {
            "test4",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = dentistEndHour,
                ServiceDuration  = TimeSpan.FromMinutes(90),
                Unavailables     = unavailables
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"), EndHour = TimeSpan.Parse("8:30") }
            }
        };

        yield return new object[]
        {
            "test5",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = TimeSpan.Parse("15:00"),
                ServiceDuration  = TimeSpan.FromMinutes(90),
                Unavailables     = unavailables
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("8:30") },
                new() { StartHour = TimeSpan.Parse("13:10"), EndHour = TimeSpan.Parse("14:40") }
            }
        };

        yield return new object[]
        {
            "test6",
            new AvailabilityOptions
            {
                DentistStartHour = TimeSpan.Parse("9:30"),
                DentistEndHour   = dentistEndHour,
                ServiceDuration  = TimeSpan.FromMinutes(30),
                Unavailables     = new List<UnavailableTimeRangeDto>
                {
                    new() { StartHour = TimeSpan.Parse("9:00"),  EndHour = TimeSpan.Parse("9:30") },
                    new() { StartHour = TimeSpan.Parse("12:40"), EndHour = TimeSpan.Parse("13:10") }
                }
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("9:30"),   EndHour = TimeSpan.Parse("10:00") },
                new() { StartHour = TimeSpan.Parse("10:00"),  EndHour = TimeSpan.Parse("10:30") },
                new() { StartHour = TimeSpan.Parse("10:30"),  EndHour = TimeSpan.Parse("11:00") },
                new() { StartHour = TimeSpan.Parse("11:00"),  EndHour = TimeSpan.Parse("11:30") },
                new() { StartHour = TimeSpan.Parse("11:30"),  EndHour = TimeSpan.Parse("12:00") },
                new() { StartHour = TimeSpan.Parse("12:00"),  EndHour = TimeSpan.Parse("12:30") }
            }
        };

        yield return new object[]
        {
            "test7",
            new AvailabilityOptions
            {
                DentistStartHour = TimeSpan.Parse("9:30"),
                DentistEndHour   = TimeSpan.Parse("14:00"),
                ServiceDuration  = TimeSpan.FromMinutes(30),
                Unavailables     = new List<UnavailableTimeRangeDto>
                {
                    new() { StartHour = TimeSpan.Parse("9:00"),  EndHour = TimeSpan.Parse("9:30") }
                }
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("9:30"),   EndHour = TimeSpan.Parse("10:00") },
                new() { StartHour = TimeSpan.Parse("10:00"),  EndHour = TimeSpan.Parse("10:30") },
                new() { StartHour = TimeSpan.Parse("10:30"),  EndHour = TimeSpan.Parse("11:00") },
                new() { StartHour = TimeSpan.Parse("11:00"),  EndHour = TimeSpan.Parse("11:30") },
                new() { StartHour = TimeSpan.Parse("11:30"),  EndHour = TimeSpan.Parse("12:00") },
                new() { StartHour = TimeSpan.Parse("12:00"),  EndHour = TimeSpan.Parse("12:30") },
                new() { StartHour = TimeSpan.Parse("12:30"),  EndHour = TimeSpan.Parse("13:00") },
                new() { StartHour = TimeSpan.Parse("13:00"),  EndHour = TimeSpan.Parse("13:30") },
                new() { StartHour = TimeSpan.Parse("13:30"),  EndHour = TimeSpan.Parse("14:00") }
            }
        };

        yield return new object[]
        {
            "test8",
            new AvailabilityOptions
            {
                DentistStartHour = dentistStartHour,
                DentistEndHour   = TimeSpan.Parse("11:00"),
                ServiceDuration  = TimeSpan.FromMinutes(30),
                Unavailables     = new List<UnavailableTimeRangeDto>
                {
                    new() { StartHour = TimeSpan.Parse("9:00"),   EndHour = TimeSpan.Parse("9:30") },
                    new() { StartHour = TimeSpan.Parse("11:00"),  EndHour = TimeSpan.Parse("11:30") },
                    new() { StartHour = TimeSpan.Parse("12:40"),  EndHour = TimeSpan.Parse("13:10") }
                }
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("7:00"),  EndHour = TimeSpan.Parse("7:30") },
                new() { StartHour = TimeSpan.Parse("7:30"),  EndHour = TimeSpan.Parse("8:00") },
                new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("8:30") },
                new() { StartHour = TimeSpan.Parse("8:30"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("9:30"),  EndHour = TimeSpan.Parse("10:00") },
                new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("10:30") },
                new() { StartHour = TimeSpan.Parse("10:30"), EndHour = TimeSpan.Parse("11:00") }
            }
        };

        yield return new object[]
        {
            "test9",
            new AvailabilityOptions
            {
                DentistStartHour = TimeSpan.Parse("8:00"),
                DentistEndHour   = TimeSpan.Parse("12:00"),
                ServiceDuration  = TimeSpan.FromMinutes(60),
                Unavailables     = new List<UnavailableTimeRangeDto>()
            },
            new List<AvailableTimeRangeDto>
            {
                new() { StartHour = TimeSpan.Parse("8:00"),  EndHour = TimeSpan.Parse("9:00") },
                new() { StartHour = TimeSpan.Parse("9:00"),  EndHour = TimeSpan.Parse("10:00") },
                new() { StartHour = TimeSpan.Parse("10:00"), EndHour = TimeSpan.Parse("11:00") },
                new() { StartHour = TimeSpan.Parse("11:00"), EndHour = TimeSpan.Parse("12:00") }
            }
        };
    }
}
