namespace DentallApp.Features.PublicHolidays;

public static class PublicHolidayMapper
{
    public static PublicHoliday MapToPublicHoliday(this PublicHolidayInsertDto holidayInsertDto)
        => new()
        {
            Description = holidayInsertDto.Description,
            Day         = holidayInsertDto.Day,
            Month       = holidayInsertDto.Month
        };

    public static void MapToPublicHoliday(this PublicHolidayUpdateDto holidayUpdateDto, PublicHoliday publicHoliday)
    {
        publicHoliday.Description = holidayUpdateDto.Description;
        publicHoliday.Day         = holidayUpdateDto.Day;
        publicHoliday.Month       = holidayUpdateDto.Month;
    }

    [Decompile]
    public static PublicHolidayGetAllDto MapToPublicHolidayGetAllDto(this PublicHoliday publicHoliday)
        => new()
        {
            Id          = publicHoliday.Id,
            Description = publicHoliday.Description,
            Day         = publicHoliday.Day,
            Month       = publicHoliday.Month,
            Offices     = publicHoliday.HolidayOffices
                                       .Select(holidayOffice => 
                                                new OfficeGetDto 
                                                { 
                                                    Id   = holidayOffice.OfficeId, 
                                                    Name = holidayOffice.Office.Name 
                                                })
        };
}
