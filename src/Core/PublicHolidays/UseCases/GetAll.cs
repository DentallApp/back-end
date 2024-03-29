﻿namespace DentallApp.Core.PublicHolidays.UseCases;

public class GetPublicHolidaysResponse
{
    public class Office
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public int Id { get; init; }
    public string Description { get; init; }
    public int Day { get; init; }
    public int Month { get; init; }
    public IEnumerable<Office> Offices { get; init; }
}

public class GetPublicHolidaysUseCase(DbContext context)
{
    public async Task<IEnumerable<GetPublicHolidaysResponse>> ExecuteAsync()
    {
        var holidays = await context.Set<PublicHoliday>()
            .Select(publicHoliday => new GetPublicHolidaysResponse
            {
                Id          = publicHoliday.Id,
                Description = publicHoliday.Description,
                Day         = publicHoliday.Day,
                Month       = publicHoliday.Month,
                Offices     = publicHoliday.Offices
                   .Select(office => new GetPublicHolidaysResponse.Office
                   {
                        Id   = office.OfficeId,
                        Name = office.Office.Name
                   })
            })
            .AsNoTracking()
            .ToListAsync();

        return holidays;
    }
}
