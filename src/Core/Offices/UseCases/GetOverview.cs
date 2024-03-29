﻿namespace DentallApp.Core.Offices.UseCases;

public class GetOfficeOverviewResponse
{
    public string Name { get; init; }
    public bool IsOfficeDeleted { get; init; }
    public List<OfficeScheduleResponse> Schedules { get; set; }
}

/// <summary>
/// Represents a use case to obtain an overview of the information of each active and inactive office.
/// </summary>
public class GetOfficeOverviewUseCase(DbContext context)
{
    public async Task<IEnumerable<GetOfficeOverviewResponse>> ExecuteAsync()
    {
        var offices = await context.Set<Office>()
            .Where(office => office.OfficeSchedules.Any())
            .Select(office => new GetOfficeOverviewResponse
            {
                Name            = office.Name,
                IsOfficeDeleted = office.IsDeleted,
                Schedules       = office.OfficeSchedules
                    .Select(officeSchedule => new OfficeScheduleResponse
                    {
                        WeekDayId   = officeSchedule.WeekDayId,
                        WeekDayName = officeSchedule.WeekDay.Name,
                        Schedule    = officeSchedule.ToString()
                    })
                    .OrderBy(schedule => schedule.WeekDayId)
                    .ToList()
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        foreach (var office in offices)
        {
            office.Schedules = office.Schedules.AddMissingSchedules();
        }

        return offices;
    }
}
