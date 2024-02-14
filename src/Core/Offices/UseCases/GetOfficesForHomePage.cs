namespace DentallApp.Core.Offices.UseCases;

public class GetOfficesForHomePageResponse
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }
    public List<OfficeScheduleResponse> Schedules { get; set; }
}

/// <summary>
/// Represents a use case to obtain the information of each active office for the home page.
/// </summary>
public class GetOfficesForHomePageUseCase(DbContext context)
{
    public async Task<IEnumerable<GetOfficesForHomePageResponse>> ExecuteAsync()
    {
        var offices = await context.Set<Office>()
            .Where(office => office.OfficeSchedules.Any())
            .Select(office => new GetOfficesForHomePageResponse
            {
                Name          = office.Name,
                Address       = office.Address,
                ContactNumber = office.ContactNumber,
                Schedules     = office.OfficeSchedules
                    .Select(officeSchedule => new OfficeScheduleResponse
                    {
                        WeekDayId   = officeSchedule.WeekDayId,
                        WeekDayName = officeSchedule.WeekDay.Name,
                        Schedule    = officeSchedule.ToString()
                    })
                    .OrderBy(schedule => schedule.WeekDayId)
                    .ToList()
            })
            .AsNoTracking()
            .ToListAsync();

        foreach (var office in offices)
        {
            office.Schedules = office.Schedules.AddMissingSchedules(message: Messages.OfficeClosed);
        }

        return offices;
    }
}
