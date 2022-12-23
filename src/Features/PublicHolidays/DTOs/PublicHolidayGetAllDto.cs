namespace DentallApp.Features.PublicHolidays.DTOs;

public class PublicHolidayGetAllDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public IEnumerable<OfficeGetDto> Offices { get; set; }
}
